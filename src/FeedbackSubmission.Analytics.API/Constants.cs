namespace FeedbackSubmission.Analytics.API;

internal static class Constants
{
    public static string DATABASE = nameof(DATABASE);
    public static string DATABASE_NAME = "feedback_analytics";

    public static string SWAGGER_API_TITLE = "Feedback Analytics API";
    public static string SWAGGER_SUMMARY = "Get summarized feedback messages";
    public static string SWAGGER_PREFIX= "api/v{apiVersion:apiVersion}/feedback/summarized";
}
