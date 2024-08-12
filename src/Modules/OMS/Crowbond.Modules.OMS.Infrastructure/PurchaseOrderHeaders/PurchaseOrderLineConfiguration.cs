using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.PurchaseOrderHeaders;

public sealed class PurchaseOrderLineConfiguration : IEntityTypeConfiguration<PurchaseOrderLine>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderLine> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.ProductSku).IsRequired().HasMaxLength(100);
        builder.Property(p => p.ProductName).IsRequired().HasMaxLength(100);
        builder.Property(p => p.UnitOfMeasureName).IsRequired().HasMaxLength(20);
        builder.Property(p => p.UnitPrice).IsRequired().HasPrecision(10, 2);
        builder.Property(p => p.Qty).IsRequired().HasPrecision(10, 2);
        builder.Property(p => p.SubTotal).IsRequired().HasPrecision(10, 2);
        builder.Property(p => p.Tax).IsRequired().HasPrecision(5, 2);
        builder.Property(p => p.LineTotal).IsRequired().HasPrecision(10, 2);
    }
}
