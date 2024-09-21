using Microsoft.EntityFrameworkCore;

namespace FeedbackSubmission.Analytics.API.Database;

public static class Extensions
{
    public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(Constants.DATABASE)!;

        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseMongoDB(connectionString, Constants.DATABASE_NAME);
            options.UseCamelCaseNamingConvention();
        });
    }
}
