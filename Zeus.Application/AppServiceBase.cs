using Microsoft.Extensions.Logging;

namespace Zeus.Application
{
    public abstract class AppServiceBase
    {
        protected readonly ILogger _logger;

        public AppServiceBase(ILogger logger)
        {
            _logger = logger;
        }

        protected void AddLog(string message)
        {
            _logger.LogInformation(message);
        }
    }
}
