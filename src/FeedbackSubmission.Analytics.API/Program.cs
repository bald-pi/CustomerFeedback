using FeedbackSubmission.Analytics.API;
using FeedbackSubmission.Analytics.API.Extensions;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharedAppConfiguration(builder, Constants.SWAGGER_API_TITLE);

builder.Services.AddRabbitMqConfiguration(builder);

builder.Services.AddSwaggerGenConfiguration(builder);

builder.Services.AddDatabaseConfiguration(builder.Configuration);

var app = builder.Build();

app.AddSharedMiddlewareConfiguration();

app.AddMiddlewareConfiguration();

await app.RunAsync();
