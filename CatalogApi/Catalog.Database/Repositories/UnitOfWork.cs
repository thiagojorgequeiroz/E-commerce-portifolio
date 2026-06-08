using Catalog.Database.Context;
using Catalog.Domain.Respositories;

namespace Catalog.Database.Repositories
{
    public class UnitOfWork(CatalogDbContext context) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
    }
}
