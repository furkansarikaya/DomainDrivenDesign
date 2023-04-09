using System.Diagnostics;

namespace Order.API.Middlewares;

public class LoggingMiddleware
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly RequestDelegate _next;

    public LoggingMiddleware(IWebHostEnvironment webHostEnvironment, RequestDelegate next)
    {
        _webHostEnvironment = webHostEnvironment;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopWatch = new Stopwatch();

        context.Response.OnStarting(() =>
        {
            stopWatch.Stop();

            context.Response.Headers.Add("ElapsedMilliseconds", new[] { stopWatch.ElapsedMilliseconds.ToString() });
            context.Response.Headers.Add("Elapsed", new[] { stopWatch.Elapsed.ToString() });
            context.Response.Headers.Add("Enviroment", new[] { _webHostEnvironment.EnvironmentName });

            if (context.Request.Headers["CorrelationId"].Any())
                context.Response.Headers.Add("CorrelationId", context.Request.Headers["CorrelationId"]);

            return Task.CompletedTask;
        });

        stopWatch.Start();

        await _next(context);
    }
}