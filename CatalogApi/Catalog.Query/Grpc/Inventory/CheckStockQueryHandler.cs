using Catalog.Application.Contract.Grpc.Inventory;
using Catalog.Database.Context;
using Catalog.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Query.Grpc.Inventory
{
    public class CheckStockQueryHandler(CatalogDbContext context) : IRequestHandler<CheckStockQueryRequest, CheckStockQueryResponse>
    {
        public async Task<CheckStockQueryResponse> Handle(CheckStockQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await context.Inventory.AsNoTracking()
                .Where(i => i.ProductId == request.ProductId)
                .Select(i => new CheckStockQueryResponse
                {
                    Available = i.QuantityAvailable >= request.Quantity
                })
                .FirstOrDefaultAsync();

            return response ?? throw new NotFoundException("Inventário de produto não encontrado.");
        }
    }
}
