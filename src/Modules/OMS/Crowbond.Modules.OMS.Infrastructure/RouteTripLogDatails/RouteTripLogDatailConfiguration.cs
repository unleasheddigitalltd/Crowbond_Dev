using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.RouteTripLogDatails;
using Crowbond.Modules.OMS.Domain.RouteTripLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.RouteTripLogDatails;

internal sealed class RouteTripLogDatailConfiguration : IEntityTypeConfiguration<RouteTripLogDatail>
{
    public void Configure(EntityTypeBuilder<RouteTripLogDatail> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasIndex(r => r.OrderHeaderId).IsUnique();

        builder.HasOne<OrderHeader>().WithMany().HasForeignKey(r => r.OrderHeaderId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne<RouteTripLog>().WithMany().HasForeignKey(r => r.RouteTripLogId).OnDelete(DeleteBehavior.NoAction);
    }
}
