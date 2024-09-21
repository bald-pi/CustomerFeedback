namespace FeedbackSubmission.API.Features.Feedback;

public static class FeedbackExtension
{
    internal static void RegisterCreateFeedbackValidator(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateFeedbackRequest>, Validator>();
    }

    internal static string[] ExtractTagsFromString(this string text)
    {
        MatchCollection tags = Regex.Matches(text, Constants.TAGS_REGEX);

        if (tags.Count == 0)
        {
            return [Constants.DEFAULT_TAG_VALUE];
        }

        return tags.Cast<Match>()
                   .Select(tag => tag.Value)
                   .ToArray();
    }

    internal static long GenerateRandomNumbers() => new Random().Next(0, 5000);
}
