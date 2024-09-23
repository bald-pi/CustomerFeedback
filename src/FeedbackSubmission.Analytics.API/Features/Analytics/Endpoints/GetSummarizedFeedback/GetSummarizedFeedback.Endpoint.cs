using FluentResults;

namespace FeedbackSubmission.Analytics.API.Features.Analytics.Endpoints.GetSummarizedFeedback;

public static class GetSummarizedFeedback
{
    public static void RegisterEndpoints(this IEndpointRouteBuilder endpoints)
    {
        Asp.Versioning.Builder.ApiVersionSet apiVersionSet = endpoints.NewApiVersionSet()
                                                                      .HasApiVersion(new ApiVersion(1))
                                                                      .ReportApiVersions()
                                                                      .Build();

        RouteGroupBuilder summarizedFeedbacks = endpoints.MapGroup("api/v{apiVersion:apiVersion}/feedback/summarized")
                                                         .WithApiVersionSet(apiVersionSet)
                                                         .WithOpenApi()
                                                         .WithTags(nameof(Analytics));

        summarizedFeedbacks.MapGet("/", Endpoint)
                           .WithSummary("Get summarized feedback messages")
                           .WithOpenApi();
    }

    private static async Task<Result<List<FeedbackAnalyticsResponse>>> Endpoint(DatabaseContext databaseContext,
                                                                                int page,
                                                                                int pageSize,
                                                                                CancellationToken cancellationToken)
    {
        List<FeedbackAnalyticsResponse> result = await databaseContext.Analytics
                                                                      .Skip((page - 1) * pageSize)
                                                                      .Take(pageSize)
                                                                      .Select(feedback => new FeedbackAnalyticsResponse(feedback.CustomerId,
                                                                                                                        feedback.FeedbackId,
                                                                                                                        feedback.SubmissionDate,
                                                                                                                        feedback.NumberOfTags))
                                                                      .ToListAsync(cancellationToken); // sreditti

        return Result.Ok(result);
    }

    // validator da ne sme da bude manje od 0
}