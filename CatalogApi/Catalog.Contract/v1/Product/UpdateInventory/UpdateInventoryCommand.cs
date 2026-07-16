using Catalog.Application.Contract.v1.Commun;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Catalog.Application.Contract.v1.Product.UpdateInventory
{
    public record UpdateInventoryCommand : IRequest<CommunMessageResponse>
    {
        [JsonIgnore]
        [BindingBehavior(BindingBehavior.Never)]
        public Guid Id { get; set; }

        public int? Quantity { get; init; }
    }

    public class UpdateInventoryCommandValidator : AbstractValidator<UpdateInventoryCommand>
    {
        public UpdateInventoryCommandValidator()
        {
            RuleFor(x => x.Quantity).NotNull().GreaterThan(-1).WithMessage("The inventory quantity is requierd and must be a postitive number");
        }
    }
}
