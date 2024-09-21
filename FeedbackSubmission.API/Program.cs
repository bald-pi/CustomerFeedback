WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppConfiguration(builder);

builder.Services.AddDatabaseConfiguration(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ApplyPendingMigration();
}

app.AddMiddlewareConfiguration();

await app.RunAsync();