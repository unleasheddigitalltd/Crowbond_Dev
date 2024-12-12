using Crowbond.Modules.WMS.Domain.Dispatches;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Dispatches;

internal sealed class DispatchHeaderConfiguration : IEntityTypeConfiguration<DispatchHeader>
{
    public void Configure(EntityTypeBuilder<DispatchHeader> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(d => d.DispatchNo).HasMaxLength(20);
        builder.Property(d => d.RouteName).HasMaxLength(100);
    }
}
