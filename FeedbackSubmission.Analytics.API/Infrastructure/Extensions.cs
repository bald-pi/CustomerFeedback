using FeedbackSubmission.Analytics.API.Contracts.Consumers;
using MassTransit;

namespace FeedbackSubmission.Analytics.API.Infrastructure;

public static class Extensions
{
    internal static void AddInfrastructureConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMassTransit(coniguration =>
        {
            coniguration.AddConsumer<FeedbackCreatedConsumer>();

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

    internal static void AddInfrastructureMiddlewareConfiguration(this WebApplication application)
    {

    }
}
