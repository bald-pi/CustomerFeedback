namespace FeedbackSubmission.Analytics.API.Features.Summary.Endpoints.GetSummarizedFeedback;

internal sealed record FeedbackAnalyticsResponse(long CustomerId,
                                                 long FeedbackId,
                                                 long SubmissionDate,
                                                 int NumberOfTags);