using Catalog.Database.Context;
using Catalog.Domain.Exceptions;
using Catalog.Domain.Respositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Catalog.Database.Repositories
{
    public class UnitOfWork(CatalogDbContext context) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pg)
            {
                throw new DatabaseException(pg.MessageText);
            }
        }
    }
}
