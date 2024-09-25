WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSharedAppConfiguration(builder, Constants.SWAGGER_API_TITLE);
builder.Services.AddRabbitMqConfiguration(builder);
builder.Services.AddSwaggerGenConfiguration(builder);
builder.Services.RegisterCreateFeedbackValidator();
builder.Services.AddDatabaseConfiguration(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ApplyPendingMigration();
}

app.AddSharedMiddlewareConfiguration();
app.RegisterEndpoints();

await app.RunAsync();

public partial class Program;