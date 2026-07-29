using FluentValidation;
using MediatR;

namespace Catalog.Application.Contract.Grpc.Inventory
{
    public class ReserveStockCommand : IRequest<ReserveStockCommandResponse>
    {
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }
    }

    public class ReserveStockCommandValidator : AbstractValidator<ReserveStockCommand>
    {
        public ReserveStockCommandValidator()
        {
            RuleFor(x => x.ProductId).NotNull().WithMessage("The product ID is required.");
            RuleFor(x => x.Quantity).NotNull().GreaterThan(0).WithMessage("The quantity must be a positive number.");
        }
    }
}
