using System.Net;
using System.Text.Json;
using CoreFinalDemo.Models;


namespace CoreFinalDemo.Middleware
{
    /// <summary>
    /// Middleware to handle exceptions globally.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private static Response _objResponse;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, Response objResponse)
        {
            _next = next;
            _logger = logger;
            _objResponse = objResponse;
        }

        /// <summary>
        /// Invokes the middleware to handle the HTTP context.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Handles the exception and returns a response.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <param name="exception">The exception that occurred.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            _objResponse.IsError = true;
            _objResponse.Message = "An unexpected error occurred. Please try again later.";
            _objResponse.Data = "Internal Server Error";

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return httpContext.Response.WriteAsync(JsonSerializer.Serialize(_objResponse));
        }
    }

    /// <summary>
    /// Extension methods for the middleware.
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Adds the exception handling middleware to the application builder.
        /// </summary>
        /// <param name="builder">The application builder.</param>
        /// <returns>The application builder.</returns>
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}