using Catalog.Application.Contract.v1.Commun;
using Catalog.Application.Contract.v1.Product.UpdateInventory;
using Catalog.Domain.Respositories;
using MediatR;

namespace Catalog.Application.Command.v1.Products
{
    public class UpdateInventoryCommandHandler(IInventoryRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateInventoryCommand, CommunMessageResponse>
    {
        public async Task<CommunMessageResponse> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            var inventory = await repository.GetById(request.Id, cancellationToken);
            inventory.UpdateQuantity(request.Quantity!.Value);
            await unitOfWork.SaveChangesAsync();

            return new CommunMessageResponse("Inventory updated successfully.");
        }
    }
}
