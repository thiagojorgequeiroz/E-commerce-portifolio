using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Catalog.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            using (_logger.BeginScope(
                new Dictionary<string, object>
                {
                    ["TraceId"] = Activity.Current?.TraceId.ToString() ?? string.Empty,
                    ["RequestType"] = typeof(TRequest).Name
                }))
            {
                try
                {
                    _logger.LogInformation(
                        "Handling request {RequestName}",
                        typeof(TRequest).Name);

                    var response = await next();

                    return response;
                }
                finally
                {
                    _logger.LogInformation(
                        "Handled {RequestType} in {ElapsedMilliseconds}ms",
                        typeof(TRequest).Name,
                        stopwatch.ElapsedMilliseconds);
                    stopwatch.Stop();
                }
            }
        }
    }
}
