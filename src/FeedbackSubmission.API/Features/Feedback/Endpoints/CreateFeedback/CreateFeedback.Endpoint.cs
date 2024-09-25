
namespace FeedbackSubmission.API.Features.Feedback.Endpoints.CreateFeedback;

public static partial class CreateFeedbackEndpoint
{
    public static void RegisterEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var apiVersionSet = endpoints.NewApiVersionSet()
                                     .HasApiVersion(new ApiVersion(1))
                                     .ReportApiVersions()
                                     .Build();

        var feedbacks = endpoints.MapGroup(Constants.SWAGGER_PREFIX)
                                 .WithApiVersionSet(apiVersionSet)
                                 .WithOpenApi()
                                 .WithTags(nameof(Feedback));

        feedbacks.MapPost("/", Endpoint)
                 .WithSummary(Constants.SWAGGER_SUMMARY)
                 .WithOpenApi()
                 .WithMetadata(new SwaggerRequestExampleAttribute(typeof(CreateFeedbackRequest), typeof(CreateFeedbackRequestExample)));
    }

    private static async Task<Result<ulong>> Endpoint(CreateFeedbackRequest request,
                                                      IConfiguration configuration,
                                                      DatabaseContext databaseContext,
                                                      IPublishEndpoint publisher,
                                                      IValidator<CreateFeedbackRequest> validator,
                                                      CancellationToken cancellationToken)
    {
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            List<string> errors = [];

            validationResult.Errors.ForEach(error => errors.Add(error.ErrorMessage));

            return Result.Fail(errors);
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

        var feedbacks = await databaseContext.Feedbacks.FromSqlInterpolated(Constants.SQL_SELECT_STATEMENT)
                                                       .AsNoTracking()
                                                       .Select(feedback => new Shared.Contracts.Feedback(feedback.Id, feedback.CustomerId, feedback.SubmissionDate, feedback.Tags!))
                                                       .ToListAsync(cancellationToken);

        await publisher.Publish(new FeedbacksCreated(feedbacks), cancellationToken);

        return Result.Ok(result);
    }
}