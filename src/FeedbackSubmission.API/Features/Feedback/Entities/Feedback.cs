namespace FeedbackSubmission.API.Features.Feedback.Entities;

public sealed class Feedback
{
    public long Id { get; init; }

    public long SubmissionDate { get; set; }

    public long CustomerId { get; init; }

    public string? FeedbackText { get; set; }

    public List<string>? Tags { get; set; }
}