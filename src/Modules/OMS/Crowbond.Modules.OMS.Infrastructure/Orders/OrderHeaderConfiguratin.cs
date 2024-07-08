using Crowbond.Modules.OMS.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.Orders;

public sealed class OrderHeaderConfiguratin : IEntityTypeConfiguration<OrderHeader>
{
    public void Configure(EntityTypeBuilder<OrderHeader> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.InvoiceNo).IsRequired().HasMaxLength(100);
        builder.Property(o => o.InvoicedBy).HasMaxLength(100);
        builder.Property(o => o.CustomerName).IsRequired().HasMaxLength(100);
        builder.Property(o => o.CustomerMobile).IsRequired().HasMaxLength(20);
        builder.Property(o => o.CustomerEmail).IsRequired().HasMaxLength(255);
        builder.Property(o => o.OrderAmount).HasPrecision(10, 2);
        builder.Property(o => o.ShippingAddressCompany).IsRequired().HasMaxLength(100);
        builder.Property(o => o.ShippingAddressLine1).IsRequired().HasMaxLength(255);
        builder.Property(o => o.ShippingAddressLine2).IsRequired().HasMaxLength(255);
        builder.Property(o => o.ShippingAddressTownCity).IsRequired().HasMaxLength(100);
        builder.Property(o => o.ShippingAddressCounty).IsRequired().HasMaxLength(100);
        builder.Property(o => o.ShippingAddressCountry).IsRequired().HasMaxLength(100);
        builder.Property(o => o.ShippingAddressPostalCode).IsRequired().HasMaxLength(20);
        builder.Property(o => o.SalesOrderNumber).IsRequired().HasMaxLength(100);
        builder.Property(o => o.PurchaseOrderNumber).IsRequired().HasMaxLength(100);
        builder.Property(o => o.OrderTax).HasPrecision(10, 2);
        builder.Property(o => o.DeliveryCharge).HasPrecision(10, 2);
        builder.Property(o => o.CustomerComment).HasMaxLength(3000);
        builder.Property(o => o.OriginalSource).IsRequired().HasMaxLength(100);
        builder.Property(o => o.ExternalOrderRef).IsRequired().HasMaxLength(100);
        builder.Property(o => o.Tags).HasMaxLength(255);
    }
}
