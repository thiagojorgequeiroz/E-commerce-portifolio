using Catalog.Application.Exceptions;
using Catalog.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CatalogWebApi.ProgramConfiguration.Exceptions
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is ValidationAppException validationException)
            {
                var problem = new ValidationProblemDetails(validationException.Errors)
                {
                    Title = validationException.Title,
                    Detail = validationException.Message,
                    Status = validationException.StatusCode,
                    Instance = httpContext.Request.Path
                };
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

                logger.LogWarning(
                    validationException,
                    validationException.Message);

                return true;
            }

            if (exception is AppException appException)
            {
                var problem = new ProblemDetails
                {
                    Title = appException.Title,
                    Detail = appException.Message,
                    Status = appException.StatusCode,
                    Instance = httpContext.Request.Path
                };
                httpContext.Response.StatusCode = appException.StatusCode;
                await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

                logger.LogWarning(
                    appException,
                    appException.Message);

                return true;
            }

            var unexpectedException = new UnexpectedException();

            var unexpectedProblem = new ProblemDetails
            {
                Title = unexpectedException.Title,
                Detail = unexpectedException.Message,
                Status = unexpectedException.StatusCode,
                Instance = httpContext.Request.Path
            };
            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsJsonAsync(unexpectedProblem, cancellationToken);

            logger.LogError(
                exception,
                unexpectedException.Message);

            return true;
        }
    }
}
