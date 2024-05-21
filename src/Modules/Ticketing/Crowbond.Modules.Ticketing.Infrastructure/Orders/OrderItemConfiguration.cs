using Crowbond.Modules.Ticketing.Domain.Events;
using Crowbond.Modules.Ticketing.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.Ticketing.Infrastructure.Orders;

internal sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).ValueGeneratedNever();

        builder.HasOne<TicketType>().WithMany().HasForeignKey(oi => oi.TicketTypeId);
    }
}
