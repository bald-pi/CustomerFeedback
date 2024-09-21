using FeedbackSubmission.Analytics.API.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureConfiguration(builder);

builder.Services.AddDatabaseConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("api/analytics", async (DatabaseContext dbContext) => { return Results.Ok(await dbContext.Analytics.ToListAsync()); });

await app.RunAsync();
