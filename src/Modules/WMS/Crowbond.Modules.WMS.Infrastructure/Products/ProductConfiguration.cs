using Crowbond.Modules.WMS.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Products;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne<UnitOfMeasure>().WithMany()
            .HasForeignKey(e => e.UnitOfMeasureName)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<FilterType>().WithMany()
            .HasForeignKey(e => e.FilterTypeName)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<InventoryType>().WithMany()
            .HasForeignKey(e => e.InventoryTypeName)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Category>().WithMany()
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Brand>().WithMany()
            .HasForeignKey(e => e.BrandId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<ProductGroup>().WithMany()
            .HasForeignKey(e => e.ProductGroupId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Product>().WithMany()
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(c => c.Sku).HasMaxLength(20);

        builder.Property(c => c.Name).HasMaxLength(150);

        builder.Property(c => c.FilterTypeName).HasMaxLength(20);

        builder.Property(c => c.UnitOfMeasureName).HasMaxLength(20);

        builder.Property(c => c.InventoryTypeName).HasMaxLength(20);

        builder.Property(c => c.HandlingNotes).HasMaxLength(500);

        builder.Property(c => c.Notes).HasMaxLength(500);

        builder.Property(c => c.Height).HasPrecision(19, 0);

        builder.Property(c => c.Width).HasPrecision(19, 0);

        builder.Property(c => c.Length).HasPrecision(19, 0);

        builder.Property(c => c.ReorderLevel).HasPrecision(10, 2);

        builder.Property(c => c.PackSize).HasPrecision(10, 2);
    }
}
