using Crowbond.Modules.WMS.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Products;

internal sealed class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

        builder
            .HasMany<ProductGroup>()
            .WithMany(c => c.Brands)
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("product_group_brands");
                
                joinBuilder.Property("BrandsId").HasColumnName("brand_id");
            });
    }
}
