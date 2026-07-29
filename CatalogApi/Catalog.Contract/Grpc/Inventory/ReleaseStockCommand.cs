using FluentValidation;
using MediatR;

namespace Catalog.Application.Contract.Grpc.Inventory
{
    public class ReleaseStockCommand : IRequest<ReleaseStockCommandResponse>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public class ReleaseStockCommandValidator : AbstractValidator<ReleaseStockCommand>
        {
            public ReleaseStockCommandValidator()
            {
                RuleFor(x => x.ProductId).NotNull().WithMessage("The product ID is required.");
                RuleFor(x => x.Quantity).NotNull().GreaterThan(0).WithMessage("The quantity must be a positive number.");
            }
        }
    }
}
