using FeedbackSubmission.API.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace FeedbackSubmission.API.Tests.Configuration;

public class AppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("feedback")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.Remove(services.SingleOrDefault(service => typeof(DbContextOptions<DatabaseContext>) == service.ServiceType));

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(_container.GetConnectionString());
                options.UseSnakeCaseNamingConvention();
            });
        });
    }

    public Task InitializeAsync() => _container.StartAsync();

    Task IAsyncLifetime.DisposeAsync() => _container.StopAsync();
}
