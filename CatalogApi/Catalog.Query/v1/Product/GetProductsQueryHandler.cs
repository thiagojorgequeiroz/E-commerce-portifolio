using Catalog.Application.Contract.v1.Product.GetProducts;
using Catalog.Database.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Query.v1.Product
{
    public class GetProductsQueryHandler(CatalogDbContext context) : IRequestHandler<GetProductsQueryRequest, GetProductsQueryResponse>
    {
        public async Task<GetProductsQueryResponse> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var totalItems = await context.Products.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize!.Value);

            var items = await context.Products
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Skip((request.Page!.Value - 1) * request.PageSize!.Value)
                .Take(request.PageSize!.Value)
                .Select(p => new GetProductsQueryResponseItems
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .ToListAsync();

            return new GetProductsQueryResponse
            {
                Items = items,
                TotalItems = totalItems,
                TotalPages = totalPages,
                NextPage = request.Page!.Value < totalPages
            };
        }
    }
}
