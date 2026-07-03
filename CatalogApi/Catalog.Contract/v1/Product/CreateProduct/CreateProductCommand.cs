using FluentValidation;
using MediatR;

namespace Catalog.Application.Contract.v1.Product.CreateProduct
{
    public record CreateProductCommand : IRequest<Guid>
    {
        public string? Name { get; init; }

        public string? Description { get; init; }

        public decimal? Price { get; init; }
    }

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("The product name is required.");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("The product description is required.");
            RuleFor(x => x.Price).NotNull().GreaterThan(0).WithMessage("The product price must be greater than zero.");
        }
    }
}
