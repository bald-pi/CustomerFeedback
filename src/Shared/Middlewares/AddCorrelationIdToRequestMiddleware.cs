using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Shared.Middlewares;

public class AddCorrelationIdToRequestMiddleware(RequestDelegate next)
{
    public Task Invoke(HttpContext context)
    {
        using (Serilog.Context.LogContext.PushProperty("X-Correlation-Id", GetCorrelationId(context)))
        {
            return next.Invoke(context);
        }
    }

    private static string GetCorrelationId(HttpContext httpContext)
    {
        httpContext.Request.Headers.TryGetValue("X-Correlation-Id", out StringValues correlationId);

        return correlationId.FirstOrDefault() ?? Guid.NewGuid().ToString();
    }
}
