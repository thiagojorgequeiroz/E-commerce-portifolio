using Catalog.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CatalogWebApi.ProgramConfiguration.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is ValidationAppException validationException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                await httpContext.Response.WriteAsJsonAsync(
                    new
                    {
                        title = validationException.Title,
                        statusCode = validationException.StatusCode,
                        detail = validationException.Message,
                        validationErrors = validationException.Errors
                    },
                    cancellationToken);

                return true;
            }

            if (exception is AppException appException)
            {
                httpContext.Response.StatusCode = appException.StatusCode;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    title = appException.Title,
                    statusCode = appException.StatusCode,
                    detail = appException.Message,
                }, cancellationToken);
                return true;
            }

            httpContext.Response.StatusCode = 500;
            var unexpectedException = new UnexpectedException();
            await httpContext.Response.WriteAsJsonAsync(new
            {
                title = unexpectedException.Title,
                statusCode = unexpectedException.StatusCode,
                detail = unexpectedException.Message,
            }, cancellationToken);

            return true;
        }
    }
}
