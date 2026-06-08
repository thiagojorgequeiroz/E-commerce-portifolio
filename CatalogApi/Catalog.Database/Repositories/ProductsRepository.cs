using Catalog.Database.Context;
using Catalog.Domain.Models.Product;
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
            await context.Products.AddAsync(product, cancellationToken  );
        }
    }
}
