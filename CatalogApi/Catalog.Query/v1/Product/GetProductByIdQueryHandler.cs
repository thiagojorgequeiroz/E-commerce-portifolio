using Catalog.Application.Contract.v1.Product.GetProductById;
using Catalog.Database.Context;
using Catalog.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Application.Query.v1.Product
{
    public class GetProductByIdQueryHandler(CatalogDbContext context) : IRequestHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>
    {
        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await context.Products
                .AsNoTracking()
                .Where(p => p.Id == request.Id)
                .Select(p => new GetProductByIdQueryResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    AvailableQuantity = p.Inventory.QuantityAvailable
                })
                .FirstOrDefaultAsync();

            if(product == null)
            {
                throw new NotFoundException($"Product with Id {request.Id} not found.");
            }

            return product!;
        }
    }
}
