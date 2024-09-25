using FeedbackSubmission.API.Tests.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FeedbackSubmission.API.Tests;

public class BaseTests : IClassFixture<AppFactory>
{
    public HttpClient HttpClient { get; }

    public BaseTests(AppFactory appFactory)
    {
        var clientOptions = new WebApplicationFactoryClientOptions() { BaseAddress = new Uri("https://localhost:5001") };

        HttpClient = appFactory.CreateClient(clientOptions);
    }
}
