namespace Shared.Contracts;

public record FeedbacksCreated(List<Feedback> Feedbacks);

public record Feedback(long FeedbackId,
                       long CustomerId,
                       long SubmissionDate,
                       List<string> Tags);
