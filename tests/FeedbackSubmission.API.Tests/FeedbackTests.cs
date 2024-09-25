using FeedbackSubmission.API.Features.Feedback.Endpoints.CreateFeedback;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace FeedbackSubmission.API.Tests;

public class FeedbackTests(Configuration.AppFactory appFactory) : BaseTests(appFactory)
{
    [Fact]
    public async Task Should_ReturnOk_WhenThereAreFeedbacks()
    {
        var request = new CreateFeedbackRequest([]);

        var response = await HttpClient.PostAsJsonAsync("/api/v1/feedback", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
