using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace SampleFive.Web.StartupCustomizations
{
    public class CustomExceptionLoggingFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionLoggingFilterAttribute> _logger;
        public CustomExceptionLoggingFilterAttribute(ILogger<CustomExceptionLoggingFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogInformation($"OnException: {context.Exception}");
            base.OnException(context);
        }
    }
}
