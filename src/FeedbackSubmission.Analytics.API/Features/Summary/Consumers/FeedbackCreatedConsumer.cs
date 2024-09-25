namespace FeedbackSubmission.Analytics.API.Features.Summary.Consumers;

public sealed class FeedbackCreatedConsumer(DatabaseContext dbContext) : IConsumer<FeedbacksCreated>
{
    public async Task Consume(ConsumeContext<FeedbacksCreated> context)
    {
        List<Entities.Summary> messages = [];

        foreach (Feedback feedback in context.Message.Feedbacks)
        {
            var isExists = await dbContext.Summaries.AnyAsync(summary => summary.FeedbackId == feedback.FeedbackId, context.CancellationToken);

            if (!isExists)
            {
                messages.Add(new Entities.Summary
                {
                    FeedbackId = feedback.FeedbackId,
                    CustomerId = feedback.CustomerId,
                    SubmissionDate = feedback.SubmissionDate,
                    NumberOfTags = feedback.Tags.Count
                });
            }
        }

        await dbContext.AddRangeAsync(messages);

        await dbContext.SaveChangesAsync();
    }
}