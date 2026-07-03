using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Database.TablesConfiguration
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("inventory");

            builder.HasKey(x => x.ProductId);

            builder.HasOne(x => x.Product)
                .WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.QuantityAvailable)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(x => x.QuantityReserved)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(x => x.UpdatedAt)
                .IsRequired();
        }
    }
}
