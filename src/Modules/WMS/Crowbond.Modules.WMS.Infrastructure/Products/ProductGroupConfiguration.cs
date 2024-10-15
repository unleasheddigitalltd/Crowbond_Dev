using Crowbond.Modules.WMS.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Products;

internal sealed class ProductGroupConfiguration : IEntityTypeConfiguration<ProductGroup>
{
    public void Configure(EntityTypeBuilder<ProductGroup> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

        builder
            .HasMany<Category>()
            .WithMany(c => c.ProductGroups)
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("category_product_groups");

                joinBuilder.Property("ProductGroupsId").HasColumnName("product_group_id");
            });
    }
}
