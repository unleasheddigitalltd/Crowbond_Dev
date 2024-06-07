using Crowbond.Modules.Products.Domain.Categories;
using Crowbond.Modules.Products.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.Products.Infrastructure.Products;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

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

        builder.HasOne<Product>().WithMany()
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(c => c.Sku).IsRequired().HasMaxLength(20);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

        builder.Property(c => c.ParentId).IsRequired(false);

        builder.Property(c => c.FilterTypeName).IsRequired().HasMaxLength(20);

        builder.Property(c => c.UnitOfMeasureName).IsRequired().HasMaxLength(20);

        builder.Property(c => c.CategoryId).IsRequired();

        builder.Property(c => c.InventoryTypeName).IsRequired().HasMaxLength(20);

        builder.Property(c => c.HandlingNotes).IsRequired(false).HasMaxLength(500);

        builder.Property(c => c.Notes).IsRequired(false).HasMaxLength(500);

        builder.Property(c => c.Height).IsRequired(false).HasPrecision(19, 0);

        builder.Property(c => c.Width).IsRequired(false).HasPrecision(19, 0);

        builder.Property(c => c.Length).IsRequired(false).HasPrecision(19, 0);

        builder.Property(c => c.Barcode).IsRequired(false);

        builder.Property(c => c.ReorderLevel).IsRequired(false).HasPrecision(10, 2);

        builder.Property(c => c.PackSize).IsRequired(false).HasPrecision(10, 2);
    }
}
