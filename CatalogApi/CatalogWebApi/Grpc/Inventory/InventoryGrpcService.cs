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

        public override async Task<ReserveStockResponse> ReserveStock(ReserveStockRequest request, ServerCallContext context)
        {
            var command = new ReserveStockCommand
            {
                ProductId = Guid.Parse(request.ProductId),
                Quantity = request.Quantity
            };
            var result = await mediator.Send(command);
            return new ReserveStockResponse
            {
                Success = result.Success
            };
        }

        public override async Task<ReleaseStockResponse> ReleaseStock(ReleaseStockRequest request, ServerCallContext context)
        {
            var command = new ReleaseStockCommand
            {
                ProductId = Guid.Parse(request.ProductId),
                Quantity = request.Quantity
            };
            var result = await mediator.Send(command);
            return new ReleaseStockResponse
            {
                Success = result.Success
            };
        }
    }
}
