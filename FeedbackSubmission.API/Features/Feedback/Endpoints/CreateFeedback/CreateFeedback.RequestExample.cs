namespace FeedbackSubmission.API.Features.Feedback.Endpoints.CreateFeedback;

internal class CreateFeedbackRequestExample : IExamplesProvider<CreateFeedbackRequest>
{
    public CreateFeedbackRequest GetExamples() => new(Constants.REQUEST_EXAMPLE);
}
