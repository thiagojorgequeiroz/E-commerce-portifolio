using Catalog.Domain.Entities;

namespace Catalog.Domain.Respositories
{
    public interface IProductsRespository
    {
        public Task CreateAsync(Product product, CancellationToken cancellationToken = default);
    }
}
