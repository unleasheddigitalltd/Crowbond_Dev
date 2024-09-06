using Crowbond.Modules.CRM.Domain.PriceTiers;
using Crowbond.Modules.CRM.Domain.ProductPrices;
using Crowbond.Modules.CRM.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.ProductPrices;

public sealed class ProductPriceConfiguration : IEntityTypeConfiguration<ProductPrice>
{
    public void Configure(EntityTypeBuilder<ProductPrice> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.BasePurchasePrice).IsRequired().HasPrecision(10, 2);
        builder.Property(p => p.SalePrice).IsRequired().HasPrecision(10, 2);

        builder.HasOne<PriceTier>().WithMany().HasForeignKey(p => p.PriceTierId).OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Product>().WithMany().HasForeignKey(e => e.ProductId).OnDelete(DeleteBehavior.NoAction);
    }
}
