using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.RouteTripLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.Orders;

internal sealed class OrderDeliveryConfiguration : IEntityTypeConfiguration<OrderDelivery>
{
    public void Configure(EntityTypeBuilder<OrderDelivery> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Comments).HasMaxLength(255);
        builder.HasIndex(r => r.OrderHeaderId).IsUnique();

        builder.HasOne<RouteTripLog>().WithMany().HasForeignKey(r => r.RouteTripLogId).OnDelete(DeleteBehavior.NoAction);
    }
}
