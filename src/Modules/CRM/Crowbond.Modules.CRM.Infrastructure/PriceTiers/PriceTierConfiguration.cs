using Crowbond.Modules.CRM.Domain.PriceTiers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.PriceTiers;

public sealed class PriceTierConfiguration : IEntityTypeConfiguration<PriceTier>
{
    public void Configure(EntityTypeBuilder<PriceTier> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(20);
    }
}
