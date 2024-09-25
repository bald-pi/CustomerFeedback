namespace FeedbackSubmission.API.Features.Feedback.Endpoints.CreateFeedback;

internal class Validator : AbstractValidator<CreateFeedbackRequest>
{
    public Validator()
    {
        RuleFor(request => request).NotNull().WithMessage(Constants.VALIDATION_NOT_NULL_REQUEST_MESSAGE);

        RuleFor(request => request.FeedbackText).NotNull().WithMessage(Constants.VALIDATION_FEEDBACK_TEXT);

        RuleFor(request => request.FeedbackText.Length).GreaterThan(0).WithMessage(Constants.VALIDATION_FEEDBACK_TEXT_LENGTH);
    }
}
