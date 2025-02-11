using CoreFinalDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ServiceStack;

namespace CoreFinalDemo.Filters
{
    /// <summary>
    /// Filter to log all method responses
    /// </summary>
    public class LoggingFilter : IActionFilter
    {
        private readonly ILogger<LoggingFilter> _logger;
        private Response _objResponse;

        public LoggingFilter(ILogger<LoggingFilter> logger, Response objResponse)
        {
            _logger = logger;
            _objResponse = objResponse;
        }

        /// <summary>
        /// Run after Action is Executed
        /// </summary>
        /// <param name="context">Action Filter context</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                _objResponse = objectResult.Value as Response;

                if (_objResponse.IsError == false )
                {
                    _logger.LogInformation(_objResponse.Message);
                }
                else
                {
                    _logger.LogWarning(_objResponse.Message);
                }
            }
        }

        /// <summary>
        /// Run when Action is Executing
        /// </summary>
        /// <param name="context">Action Filter context</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {

            _logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' has starting.");
        }
    }
}
