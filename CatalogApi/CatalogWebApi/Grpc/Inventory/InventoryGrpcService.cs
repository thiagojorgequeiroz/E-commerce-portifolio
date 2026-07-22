using Catalog.Application.Contract.Grpc.Inventory;
using Grpc.Core;
using MediatR;

namespace CatalogWebApi.Grpc.Inventory
{
    public class InventoryGrpcService(IMediator mediator) : InventoryService.InventoryServiceBase
    {
        public override async Task<CheckStockResponse> CheckStock(CheckStockRequest request, ServerCallContext context)
        {
            var query = new CheckStockQueryRequest
            {
                ProductId = Guid.Parse(request.ProductId),
                Quantity = request.Quantity
            };
            var result = await mediator.Send(query);
            return new CheckStockResponse
            {
                Available = result.Available
            };
        }
    }
}
