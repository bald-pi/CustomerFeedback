namespace FeedbackSubmission.API.Features.Feedback.Endpoints.CreateFeedback;

internal class Validator : AbstractValidator<CreateFeedbackRequest>
{
    public Validator()
    {
        RuleFor(request => request).NotNull().WithMessage("The request cannot be null.");

        RuleFor(request => request.FeedbackText).NotNull().WithMessage("The array cannot be null.");

        RuleFor(request => request.FeedbackText.Length).GreaterThan(0).WithMessage("The array must contain at least one element.");
    }
}
