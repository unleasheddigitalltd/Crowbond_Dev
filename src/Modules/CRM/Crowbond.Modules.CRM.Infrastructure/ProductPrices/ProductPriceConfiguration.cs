using Crowbond.Modules.CRM.Domain.Categories;
using Crowbond.Modules.CRM.Domain.PriceTiers;
using Crowbond.Modules.CRM.Domain.ProductPrices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.ProductPrices;

public sealed class ProductPriceConfiguration : IEntityTypeConfiguration<ProductPrice>
{
    public void Configure(EntityTypeBuilder<ProductPrice> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(c => c.ProductSku).IsRequired().HasMaxLength(20);
        builder.Property(c => c.ProductName).IsRequired().HasMaxLength(100);
        builder.Property(c => c.UnitOfMeasureName).IsRequired().HasMaxLength(20);
        builder.Property(c => c.CategoryId).IsRequired();
        builder.Property(p => p.BasePurchasePrice).IsRequired().HasPrecision(10, 2);
        builder.Property(p => p.SalePrice).IsRequired().HasPrecision(10, 2);

        builder.HasOne<PriceTier>().WithMany().HasForeignKey(p => p.PriceTierId).OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Category>().WithMany().HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.NoAction);
    }
}
