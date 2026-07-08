using Catalog.Application.Contract.v1.Commun;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Catalog.Application.Contract.v1.Product.UpdateProduct
{
    public record UpdateProductCommand : IRequest<CommunMessageResponse>
    {
        [JsonIgnore]
        [BindingBehavior(BindingBehavior.Never)]
        public Guid Id { get; set; }

        public string? Name { get; init; }

        public string? Description { get; init; }

        public decimal? Price { get; init; }

        public bool? IsActive { get; init; }
    }

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("The product name is required.");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("The product description is required.");
            RuleFor(x => x.Price).NotNull().GreaterThan(0).WithMessage("The product price must be greater than zero.");
        }
    }
}
