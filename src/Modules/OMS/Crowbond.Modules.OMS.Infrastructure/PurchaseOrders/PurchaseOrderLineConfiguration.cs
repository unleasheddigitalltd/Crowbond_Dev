using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.PurchaseOrders;

public sealed class PurchaseOrderLineConfiguration : IEntityTypeConfiguration<PurchaseOrderLine>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderLine> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.ProductSku).HasMaxLength(100);
        builder.Property(p => p.ProductName).HasMaxLength(150);
        builder.Property(p => p.UnitOfMeasureName).HasMaxLength(20);
        builder.Property(p => p.CategoryName).HasMaxLength(100);
        builder.Property(p => p.BrandName).HasMaxLength(100);
        builder.Property(p => p.ProductGroupName).HasMaxLength(100);
        builder.Property(p => p.UnitPrice).HasPrecision(10, 2);
        builder.Property(p => p.Qty).HasPrecision(10, 2);
        builder.Property(p => p.SubTotal).HasPrecision(10, 2);
        builder.Property(p => p.Tax).HasPrecision(5, 2);
        builder.Property(p => p.LineTotal).HasPrecision(10, 2);
    }
}
