using Catalog.Database.Context;
using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Catalog.Domain.Respositories;

namespace Catalog.Database.Repositories
{
    public class ProductsRepository
        (
            CatalogDbContext context
        ) : IProductsRespository
    {

        public async Task CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
            await context.Products.AddAsync(product, cancellationToken);
        }

        public async Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Products.FindAsync(id, cancellationToken) ?? throw new NotFoundException("Product not found"); ;
        }
    }
}
