namespace FeedbackSubmission.API.Extensions;

internal static class DatabaseConfiguration
{
    internal static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(Constants.DATABASE)!)
                   .UseSnakeCaseNamingConvention()
                   .ReplaceService<IHistoryRepository, HistoryTableRepository>();
        });

        return services;
    }

    internal static void ApplyPendingMigration(this IApplicationBuilder applicationBuilder)
    {
        using IServiceScope scope = applicationBuilder.ApplicationServices.CreateScope();

        DatabaseContext context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        context.Database.Migrate();
    }
}