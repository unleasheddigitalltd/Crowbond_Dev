using Crowbond.Modules.Ticketing.Domain.Orders;
using Crowbond.Modules.Ticketing.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.Ticketing.Infrastructure.Payments;

internal sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne<Order>().WithMany().HasForeignKey(p => p.OrderId);

        builder.HasIndex(p => p.TransactionId).IsUnique();
    }
}
