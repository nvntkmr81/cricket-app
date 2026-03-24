using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Core.Extensions
{
    internal sealed class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionLoggingMiddleware> _logger;

        public ExceptionLoggingMiddleware(RequestDelegate next, ILogger<ExceptionLoggingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Log the exception with request information
                _logger.LogError(ex, "Unhandled exception while processing request {Method} {Path}", context.Request.Method, context.Request.Path);

                if (!context.Response.HasStarted)
                {
                    context.Response.Clear();
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var payload = JsonSerializer.Serialize(new { error = "An unexpected error occurred." });
                    await context.Response.WriteAsync(payload).ConfigureAwait(false);
                }
            }
        }
    }

    public static class ExceptionLoggingExtensions
    {
        public static IApplicationBuilder UseExceptionLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionLoggingMiddleware>();
        }

        public static WebApplication UseExceptionLogging(this WebApplication app)
        {
            app.UseMiddleware<ExceptionLoggingMiddleware>();
            return app;
        }
    }
}
