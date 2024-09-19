using Crowbond.Modules.OMS.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.Orders;

public sealed class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(o => o.ProductSku).IsRequired().HasMaxLength(100);
        builder.Property(o => o.ProductName).IsRequired().HasMaxLength(100);
        builder.Property(o => o.UnitOfMeasureName).IsRequired().HasMaxLength(20);
        builder.Property(o => o.UnitPrice).IsRequired().HasPrecision(10, 2);
        builder.Property(o => o.Qty).IsRequired().HasPrecision(10, 2);
        builder.Property(o => o.SubTotal).IsRequired().HasPrecision(10, 2);
        builder.Property(o => o.Tax).IsRequired().HasPrecision(5, 2);
        builder.Property(o => o.LineTotal).IsRequired().HasPrecision(10, 2);
    }
}
