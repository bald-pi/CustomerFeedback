namespace FeedbackSubmission.API.Extensions;

internal static class AppConfiguration
{
    internal static void AddAppConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.RegisterApiVersioning()
                .RegisterMassTransit(builder);

        services.RegisterCreateFeedbackValidator();
    }

    internal static void AddMiddlewareConfiguration(this WebApplication application)
    {
        application.RegisterEndpoints();

        application.UseHttpsRedirection();
        application.UseSwagger(options => options.RouteTemplate = "api/docs/{documentName}/swagger.json");
        application.UseSwaggerUI(options =>
        {
            options.RoutePrefix = "api/docs";
            options.SwaggerEndpoint("/api/docs/v1/swagger.json", "v1");
        });
    }

    private static IServiceCollection RegisterApiVersioning(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer()
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new()
                    {
                        Title = "Feedback Submission API",
                        Version = "v1"
                    });

                    options.ExampleFilters();
                })
                .AddSwaggerExamplesFromAssemblyOf<Program>();

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    private static void RegisterMassTransit(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddMassTransit(coniguration =>
        {
            coniguration.SetKebabCaseEndpointNameFormatter();

            coniguration.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(builder.Configuration["RabbitMQ:Url"]!), h =>
                {
                    h.Username(builder.Configuration["RabbitMQ:Username"]!);
                    h.Username(builder.Configuration["RabbitMQ:Password"]!);
                });

                configurator.ConfigureEndpoints(context);
            });
        });
    }
}
