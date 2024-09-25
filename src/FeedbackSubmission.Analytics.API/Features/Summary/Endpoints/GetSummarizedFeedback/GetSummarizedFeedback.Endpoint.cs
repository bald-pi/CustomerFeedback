namespace FeedbackSubmission.Analytics.API.Features.Summary.Endpoints.GetSummarizedFeedback;

public static class GetSummarizedFeedback
{
    public static void RegisterEndpoints(this IEndpointRouteBuilder endpoints)
    {
        Asp.Versioning.Builder.ApiVersionSet apiVersionSet = endpoints.NewApiVersionSet()
                                                                      .HasApiVersion(new ApiVersion(1))
                                                                      .ReportApiVersions()
                                                                      .Build();

        RouteGroupBuilder summarizedFeedbacks = endpoints.MapGroup(Constants.SWAGGER_PREFIX)
                                                         .WithApiVersionSet(apiVersionSet)
                                                         .WithOpenApi()
                                                         .WithTags(nameof(Summary));

        summarizedFeedbacks.MapGet("/", Endpoint)
                           .WithSummary(Constants.SWAGGER_SUMMARY)
                           .WithOpenApi();
    }

    private static async Task<Result<List<FeedbackAnalyticsResponse>>> Endpoint(DatabaseContext databaseContext,
                                                                                CancellationToken cancellationToken,
                                                                                int page = 1,
                                                                                int pageSize = 15)
    {
        List<FeedbackAnalyticsResponse> result = await databaseContext.Summaries
                                                                      .Skip((page - 1) * pageSize)
                                                                      .Take(pageSize)
                                                                      .Select(feedback => new FeedbackAnalyticsResponse(feedback.CustomerId,
                                                                                                                        feedback.FeedbackId,
                                                                                                                        feedback.SubmissionDate,
                                                                                                                        feedback.NumberOfTags))
                                                                      .ToListAsync(cancellationToken);

        return Result.Ok(result);
    }
}