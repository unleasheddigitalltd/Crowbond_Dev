using Crowbond.Modules.CRM.Domain.Categories;
using Crowbond.Modules.CRM.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Products;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(c => c.Id);

        builder.HasOne<Category>().WithMany()
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(c => c.Sku).IsRequired().HasMaxLength(20);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

        builder.Property(c => c.UnitOfMeasureName).IsRequired().HasMaxLength(20);

        builder.Property(c => c.CategoryId).IsRequired();

    }
}
