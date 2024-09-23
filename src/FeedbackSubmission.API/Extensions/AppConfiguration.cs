namespace FeedbackSubmission.API.Extensions;

internal static class AppConfiguration
{
    internal static void AddRabbitMqConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddMassTransit(coniguration =>
        {
            coniguration.SetKebabCaseEndpointNameFormatter();

            coniguration.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(builder.Configuration[Shared.Constants.CONFIG_RABITTMQ_URL]!), h =>
                {
                    h.Username(builder.Configuration[Shared.Constants.CONFIG_RABITTMQ_USERNAME]!);
                    h.Username(builder.Configuration[Shared.Constants.CONFIG_RABITTMQ_PASSWORD]!);
                });

                configurator.ConfigureEndpoints(context);
            });
        });
    }

    internal static void AddSwaggerGenConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddEndpointsApiExplorer()
               .AddSwaggerGen(options =>
               {
                   options.SwaggerDoc(Shared.Constants.SWAGGER_VERSION, new()
                   {
                       Title = Constants.SWAGGER_API_TITLE,
                       Version = Shared.Constants.SWAGGER_VERSION
                   });

                   options.ExampleFilters();

               });

        services.AddSwaggerExamplesFromAssemblyOf<Program>();
    }
}
