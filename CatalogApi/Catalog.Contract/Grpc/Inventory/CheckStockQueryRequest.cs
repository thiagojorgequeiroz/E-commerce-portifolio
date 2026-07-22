using MediatR;

namespace Catalog.Application.Contract.Grpc.Inventory
{
    public class CheckStockQueryRequest : IRequest<CheckStockQueryResponse>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
