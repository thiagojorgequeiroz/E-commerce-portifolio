using Catalog.Domain.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Database.Context
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(
            DbContextOptions<CatalogDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(CatalogDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
