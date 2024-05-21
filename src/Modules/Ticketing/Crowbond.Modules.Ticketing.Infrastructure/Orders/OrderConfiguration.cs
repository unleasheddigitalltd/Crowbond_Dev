using Crowbond.Modules.Ticketing.Domain.Customers;
using Crowbond.Modules.Ticketing.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.Ticketing.Infrastructure.Orders;

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne<Customer>().WithMany().HasForeignKey(o => o.CustomerId);

        builder.HasMany(o => o.OrderItems).WithOne().HasForeignKey(oi => oi.OrderId);
    }
}
