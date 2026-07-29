using Catalog.Application.Contract.Grpc.Inventory;
using Catalog.Domain.Respositories;
using MediatR;

namespace Catalog.Application.Command.Grpc.Inventory
{
    public class ReleaseStockCommandHandler(IInventoryRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<ReleaseStockCommand, ReleaseStockCommandResponse>
    {
        public async Task<ReleaseStockCommandResponse> Handle(ReleaseStockCommand request, CancellationToken cancellationToken)
        {
            var inventory = await repository.GetById(request.ProductId, cancellationToken);
            inventory.ReleaseStock(request.Quantity);
            await unitOfWork.SaveChangesAsync();
            return new ReleaseStockCommandResponse { Success = true };
        }
    }
}
