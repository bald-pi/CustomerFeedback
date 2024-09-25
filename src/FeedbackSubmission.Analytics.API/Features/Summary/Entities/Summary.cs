namespace FeedbackSubmission.Analytics.API.Features.Summary.Entities;

public class Summary
{
    public ObjectId Id { get; set; }
    public long CustomerId { get; set; }
    public long FeedbackId { get; set; }
    public long SubmissionDate { get; set; }
    public int NumberOfTags { get; set; }
}