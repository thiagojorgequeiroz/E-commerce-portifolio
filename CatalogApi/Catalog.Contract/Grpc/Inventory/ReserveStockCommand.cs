using Catalog.Application.Contract.v1.Product.UpdateInventory;
using FluentValidation;
using MediatR;

namespace Catalog.Application.Contract.Grpc.Inventory
{
    public class ReserveStockCommand : IRequest<ReserveStockCommandResponse>
    {
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }
    }

    public class ReserveStockCommandValidator : AbstractValidator<UpdateInventoryCommand>
    {
        public ReserveStockCommandValidator()
        {
            RuleFor(x => x.Quantity).NotNull().GreaterThan(0).WithMessage("The inventory quantity to be reserved is requierd and must be greater than 0");
        }
    }
}
