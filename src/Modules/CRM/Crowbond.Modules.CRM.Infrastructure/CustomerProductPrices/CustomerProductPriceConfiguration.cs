using Crowbond.Modules.CRM.Domain.CustomerProductPrices;
using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerProductPrices;

internal sealed class CustomerProductPriceConfiguration : IEntityTypeConfiguration<CustomerProductPrice>
{
    public void Configure(EntityTypeBuilder<CustomerProductPrice> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.FixedPrice).IsRequired(false).HasPrecision(10, 2);
        builder.Property(c => c.FixedDiscount).IsRequired(false).HasPrecision(5, 2);
        builder.Property(c => c.ExpiryDate).IsRequired(false);

        builder.HasOne<CustomerProduct>().WithMany().HasForeignKey(c => c.CustomerProductId);
    }
}
