using Catalog.Domain.Entities;

namespace Catalog.Domain.Respositories
{
    public interface IInventoryRepository
    {
        Task<Inventory> GetById(Guid id, CancellationToken cancellationToken);
    }
}
