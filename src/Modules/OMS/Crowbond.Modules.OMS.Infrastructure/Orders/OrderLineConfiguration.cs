using Crowbond.Modules.OMS.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.Orders;

public sealed class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(o => o.ProductSku).HasMaxLength(100);
        builder.Property(o => o.ProductName).HasMaxLength(150);
        builder.Property(o => o.UnitOfMeasureName).HasMaxLength(20);
        builder.Property(o => o.CategoryName).HasMaxLength(100);
        builder.Property(o => o.BrandName).HasMaxLength(100);
        builder.Property(o => o.ProductGroupName).HasMaxLength(100);
        builder.Property(o => o.UnitPrice).HasPrecision(10, 2);
        builder.Property(o => o.Qty).HasPrecision(10, 2);
        builder.Property(o => o.SubTotal).HasPrecision(10, 2);
        builder.Property(o => o.Tax).HasPrecision(5, 2);
        builder.Property(o => o.LineTotal).HasPrecision(10, 2);

        builder.HasOne(ol => ol.Header)
               .WithMany(oh => oh.Lines)
               .HasForeignKey(ol => ol.OrderHeaderId)
               .IsRequired();
    }
}
