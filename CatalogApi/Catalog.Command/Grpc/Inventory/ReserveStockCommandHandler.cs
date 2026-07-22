using Catalog.Application.Contract.Grpc.Inventory;
using Catalog.Domain.Respositories;
using MediatR;

namespace Catalog.Application.Command.Grpc.Inventory
{
    public class ReserveStockCommandHandler(IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork) : IRequestHandler<ReserveStockCommand, ReserveStockCommandResponse>
    {
        public async Task<ReserveStockCommandResponse> Handle(ReserveStockCommand request, CancellationToken cancellationToken)
        {
            var inventory = await inventoryRepository.GetById(request.ProductId!.Value, cancellationToken);
            inventory.ReserveStock(request.Quantity!.Value);
            await unitOfWork.SaveChangesAsync();
            return new ReserveStockCommandResponse { Success = true };
        }
    }
}
