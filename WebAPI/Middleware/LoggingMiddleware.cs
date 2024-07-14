
namespace WebAPI.Middleware;

public class LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {

        var scope = new Dictionary<string, object> { 
            { "TraceID", httpContext.TraceIdentifier },
            { "Test", Guid.NewGuid() } };

        using (logger.BeginScope(scope))
        {
            await next(httpContext);
        }
    }
}