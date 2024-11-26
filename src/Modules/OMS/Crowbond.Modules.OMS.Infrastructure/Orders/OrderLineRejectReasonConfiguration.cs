using Crowbond.Modules.OMS.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.Orders;

internal sealed class OrderLineRejectReasonConfiguration : IEntityTypeConfiguration<OrderLineRejectReason>
{
    public void Configure(EntityTypeBuilder<OrderLineRejectReason> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Title).HasMaxLength(100);
    }
}
