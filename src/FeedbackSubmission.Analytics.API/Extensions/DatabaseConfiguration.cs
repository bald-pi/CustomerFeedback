namespace FeedbackSubmission.Analytics.API.Extensions;

internal static class DatabaseConfiguration
{
    internal static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseMongoDB(configuration.GetConnectionString(Constants.DATABASE)!, Constants.DATABASE_NAME);
            options.UseCamelCaseNamingConvention();
        });

        return services;
    }
}