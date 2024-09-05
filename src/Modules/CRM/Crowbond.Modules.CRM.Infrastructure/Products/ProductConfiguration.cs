using Crowbond.Modules.CRM.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.Products;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Sku).IsRequired().HasMaxLength(20);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.FilterTypeName).IsRequired().HasMaxLength(20);
        builder.Property(c => c.UnitOfMeasureName).IsRequired().HasMaxLength(20);
        builder.Property(c => c.InventoryTypeName).IsRequired().HasMaxLength(20);
        builder.Property(c => c.CategoryId).IsRequired();
        builder.Property(c => c.CategoryName).IsRequired().HasMaxLength(100);
    }
}
