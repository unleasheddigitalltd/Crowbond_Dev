using Crowbond.Modules.WMS.Domain.Dispatches;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Dispatches;

internal sealed class DispatchLineConfiguration : IEntityTypeConfiguration<DispatchLine>
{
    public void Configure(EntityTypeBuilder<DispatchLine> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(d => d.Qty).HasPrecision(10, 2);
    }
}
