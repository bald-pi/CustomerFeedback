namespace FeedbackSubmission.Analytics.API.Features.Analytics.Endpoints.GetSummarizedFeedback;

internal sealed record FeedbackAnalyticsResponse(long CustomerId,
                                                 long FeedbackId,
                                                 long SubmissionDate,
                                                 int NumberOfTags);