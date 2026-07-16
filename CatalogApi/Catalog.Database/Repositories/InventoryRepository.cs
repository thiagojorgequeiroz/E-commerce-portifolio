using Catalog.Database.Context;
using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Catalog.Domain.Respositories;

namespace Catalog.Database.Repositories
{
    public class InventoryRepository(CatalogDbContext context) : IInventoryRepository
    {
        public async Task<Inventory> GetById(Guid id, CancellationToken cancellationToken)
        {
            return await context.Inventory.FindAsync(id, cancellationToken) ?? throw new NotFoundException("Inventory not found");
        }
    }
}
