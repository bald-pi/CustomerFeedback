using FeedbackSubmission.Analytics.API.Database;
using FeedbackSubmission.Analytics.API.Features.Analytics.Database;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Contracts;

namespace FeedbackSubmission.Analytics.API.Contracts.Consumers;

public sealed class FeedbackCreatedConsumer(DatabaseContext dbContext) : IConsumer<FeedbacksCreated>
{
    public async Task Consume(ConsumeContext<FeedbacksCreated> context)
    {
        List<Analytic> messages = [];

        foreach (Feedback feedback in context.Message.Feedbacks)
        {
            var isExists = await dbContext.Analytics.AnyAsync(analytic => analytic.FeedbackId == feedback.FeedbackId, context.CancellationToken);

            if (!isExists)
            {
                messages.Add(new Analytic
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