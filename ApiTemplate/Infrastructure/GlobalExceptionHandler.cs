#region usings -----------------------------------------------------------------

using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace ApiTemplate.Infrastructure
{
    /// <summary>
    /// A global exception handler for the API that implements the 
    /// <see cref="IExceptionHandler"/> interface. This class provides 
    /// centralized exception management, logging, and formatting of error 
    /// responses to API consumers.
    /// </summary>
    public class GlobalExceptionHandler: IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// handles exceptions globally across the API. It logs the exception, formats 
        /// it into a <see cref="ProblemDetails"/> structure for consistency, and writes the 
        /// formatted problem details as JSON to the HTTP response.
        /// </summary>
        /// <param name="httpContext">The <see cref="HttpContext"/> for the current request.</param>
        /// <param name="exception">The exception to handle.</param>
        /// <param name="cancellationToken">A token for monitoring cancellation requests.</param>
        /// <returns>
        /// A <see cref="ValueTask{bool}"/> representing the asynchronous operation, 
        /// with a boolean result indicating whether the exception was handled.
        /// </returns>
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Exception occured: {Message}", exception.Message);

            ProblemDetails details = new ProblemDetails
            {
                Title = "API Error",
                Instance = httpContext.Request.Path,
                Status = StatusCodes.Status500InternalServerError,
                Detail = exception.Message,
                Type = "Server Error"
            };

            string response = JsonSerializer.Serialize(details);
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsync(response, cancellationToken);

            return true;
        }
    }
}
