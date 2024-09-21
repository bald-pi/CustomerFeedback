namespace FeedbackSubmission.API.Features.Feedback.Endpoints.CreateFeedback;

public static partial class CreateFeedbackEndpoint
{
    public static void RegisterEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var apiVersionSet = endpoints.NewApiVersionSet()
                                     .HasApiVersion(new ApiVersion(1))
                                     .ReportApiVersions()
                                     .Build();

        var feedbacks = endpoints.MapGroup("api/v{apiVersion:apiVersion}/feedback")
                                 .WithApiVersionSet(apiVersionSet)
                                 .WithOpenApi()
                                 .WithTags(nameof(Feedback));

        feedbacks.MapPost("/", Endpoint)
                 .WithSummary("Insert feedback messages")
                 .WithOpenApi();
    }

    private static async Task<Results<Ok<ulong>, ValidationProblem>> Endpoint(CreateFeedbackRequest request,
                                                                              IConfiguration configuration,
                                                                              DatabaseContext databaseContext,
                                                                              IPublishEndpoint publisher,
                                                                              IValidator<CreateFeedbackRequest> validator,
                                                                              CancellationToken cancellationToken)
    {
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        }

        NpgsqlDataSource source = NpgsqlDataSource.Create(configuration.GetConnectionString(Constants.DATABASE)!);

        NpgsqlConnection connection = await source.OpenConnectionAsync(cancellationToken)!;

        using NpgsqlBinaryImporter writer = await connection.BeginBinaryImportAsync(Constants.SQL_COPY_STATEMENT, cancellationToken: cancellationToken);

        foreach (string feedback in request.FeedbackText)
        {
            await writer.StartRowAsync(cancellationToken);

            await writer.WriteAsync(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()!, NpgsqlDbType.Bigint, cancellationToken);
            await writer.WriteAsync(FeedbackExtension.GenerateRandomNumbers(), NpgsqlDbType.Bigint, cancellationToken);
            await writer.WriteAsync(feedback, NpgsqlDbType.Text, cancellationToken);
            await writer.WriteAsync(feedback.ExtractTagsFromString(), npgsqlDbType: NpgsqlDbType.Array | NpgsqlDbType.Text, cancellationToken);
        }

        var result = await writer.CompleteAsync(cancellationToken).ConfigureAwait(false);

        var feedbacks = await databaseContext.Feedbacks.FromSqlInterpolated($"SELECT id, submission_date, customer_id, tags FROM feedbacks ORDER BY id")
                                                       .AsNoTracking()
                                                       .Select(feedback => new Shared.Contracts.Feedback(feedback.Id, feedback.CustomerId, feedback.SubmissionDate, feedback.Tags))
                                                       .ToListAsync(cancellationToken);

        await publisher.Publish(new FeedbacksCreated(feedbacks), cancellationToken);

        return TypedResults.Ok(result);
    }
}