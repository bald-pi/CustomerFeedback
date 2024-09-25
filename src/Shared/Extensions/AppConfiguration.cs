using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Shared.Middlewares;

namespace Shared.Extensions;

public static class AppConfiguration
{
    public static void AddSharedAppConfiguration(this IServiceCollection services, WebApplicationBuilder builder, string swagggerDocumentTitle)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();

        services.RegisterApiVersioning(swagggerDocumentTitle);

        services.RegisterOpenTelemetry(swagggerDocumentTitle);

        builder.ConfigureSerilog();
    }

    public static void AddSharedMiddlewareConfiguration(this WebApplication application)
    {
        application.UseHttpsRedirection();

        application.UseSwagger(options => options.RouteTemplate = Constants.SWAGGER_ROUTE_TEMPLATE);

        application.UseSwaggerUI(options =>
        {
            options.RoutePrefix = Constants.SWAGGER_ROUTE_PREFIX;
            options.SwaggerEndpoint(Constants.SWAGGER_URL, Constants.SWAGGER_VERSION);
        });

        application.UseSerilogRequestLogging();

        application.UseMiddleware<AddCorrelationIdToRequestMiddleware>();
    }

    private static IServiceCollection RegisterApiVersioning(this IServiceCollection services, string swagggerDocumentTitle)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = Constants.SWAGGER_GROUP_NAME;
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    private static IServiceCollection RegisterOpenTelemetry(this IServiceCollection services, string serviceName)
    {
        services.AddOpenTelemetry().ConfigureResource(resources => resources.AddService(serviceName))
                                   .WithTracing(builder =>
                                   {
                                       builder.AddHttpClientInstrumentation();
                                       builder.AddAspNetCoreInstrumentation();
                                       builder.AddNpgsql();
                                       builder.AddEntityFrameworkCoreInstrumentation();
                                       builder.AddSource(MassTransit.Logging.DiagnosticHeaders.DefaultListenerName);

                                       builder.AddOtlpExporter();
                                   });

        

        return services;
    }


    private static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(context.Configuration);
        });
    }
}
