using Crowbond.Modules.OMS.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.Orders;

internal sealed class OrderLineRejectConfiguration : IEntityTypeConfiguration<OrderLineReject>
{
    public void Configure(EntityTypeBuilder<OrderLineReject> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Qty).HasPrecision(10, 2);
        builder.Property(r => r.Comments).HasMaxLength(255);

        builder.HasOne<OrderLineRejectReason>().WithMany().HasForeignKey(r => r.RejectReasonId);
    }
}
