using Crowbond.Modules.Products.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.Products.Infrastructure.Products;

internal sealed class InventoryTypeConfiguration : IEntityTypeConfiguration<InventoryType>
{
    public void Configure(EntityTypeBuilder<InventoryType> builder)
    {
        builder.ToTable("inventory_types");

        builder.HasKey(c => c.Name);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(20);

        builder.HasData(
            InventoryType.Exclusive,
            InventoryType.Standard);
    }
}
