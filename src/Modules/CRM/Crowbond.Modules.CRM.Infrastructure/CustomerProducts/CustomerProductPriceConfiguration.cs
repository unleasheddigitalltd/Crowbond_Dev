using Crowbond.Modules.CRM.Domain.CustomerProducts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerProducts;

internal sealed class CustomerProductPriceConfiguration : IEntityTypeConfiguration<CustomerProductPrice>
{
    public void Configure(EntityTypeBuilder<CustomerProductPrice> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(c => c.FixedPrice).HasPrecision(10, 2);
        builder.Property(c => c.FixedDiscount).HasPrecision(5, 2);
        builder.Property(c => c.Comments).HasMaxLength(255);
    }
}
