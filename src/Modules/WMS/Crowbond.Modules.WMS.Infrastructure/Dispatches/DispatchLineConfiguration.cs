using Crowbond.Modules.WMS.Domain.Dispatches;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Dispatches;

internal sealed class DispatchLineConfiguration : IEntityTypeConfiguration<DispatchLine>
{
    public void Configure(EntityTypeBuilder<DispatchLine> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(d => d.OrderNo).HasMaxLength(20);
        builder.Property(d => d.CustomerBusinessName).HasMaxLength(100);
        builder.Property(d => d.OrderedQty).HasPrecision(10, 2);
        builder.Property(d => d.PickedQty).HasPrecision(10, 2);
    }
}
