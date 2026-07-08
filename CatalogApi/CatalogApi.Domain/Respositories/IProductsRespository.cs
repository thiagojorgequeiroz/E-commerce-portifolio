using Catalog.Domain.Entities;

namespace Catalog.Domain.Respositories
{
    public interface IProductsRespository
    {
        Task CreateAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
