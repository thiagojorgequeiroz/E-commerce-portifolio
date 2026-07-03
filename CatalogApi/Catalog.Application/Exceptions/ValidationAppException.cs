using Catalog.Domain.Exceptions;
using FluentValidation.Results;

namespace Catalog.Application.Exceptions
{
    public class ValidationAppException : AppException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationAppException(IEnumerable<ValidationFailure> failures) : base("Validation failed", "Validation failed", 400)
        {
            Errors = failures
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray());
        }
    }
}
