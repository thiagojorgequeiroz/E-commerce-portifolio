using Catalog.Domain.Entities;
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
        public DbSet<Inventory> Inventory => Set<Inventory>();

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(CatalogDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
