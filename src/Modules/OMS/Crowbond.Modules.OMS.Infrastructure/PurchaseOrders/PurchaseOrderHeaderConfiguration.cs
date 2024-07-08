using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.PurchaseOrders;

public sealed class PurchaseOrderHeaderConfiguratin : IEntityTypeConfiguration<PurchaseOrderHeader>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderHeader> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.PurchaseOrderNo).IsRequired().HasMaxLength(100);
        //builder.Property(o => o.InvoicedBy).
        //builder.Property(o => o.InvoicedDate).
        //builder.Property(o => o.CustomerId).
        //builder.Property(o => o.CustomerName).
        //builder.Property(o => o.CustomerMobile).
        //builder.Property(o => o.CustomerEmail).
        //builder.Property(o => o.OrderAmount).
        //builder.Property(o => o.DriverCode).
        //builder.Property(o => o.ShippingAddressCompany).
        //builder.Property(o => o.ShippingAddressLine1).
        //builder.Property(o => o.ShippingAddressLine2).
        //builder.Property(o => o.ShippingAddressTownCity).
        //builder.Property(o => o.ShippingAddressCounty).
        //builder.Property(o => o.ShippingAddressCountry).
        //builder.Property(o => o.ShippingAddressPostalCode).
        //builder.Property(o => o.ShippingDate).
        //builder.Property(o => o.SalesOrderNumber).
        //builder.Property(o => o.PurchaseOrderNumber).
        //builder.Property(o => o.OrderTax).
        //builder.Property(o => o.DeliveryMethod).
        //builder.Property(o => o.DeliveryCharge).
        //builder.Property(o => o.PaymentMethod).
        //builder.Property(o => o.PaymentStatus).
        //builder.Property(o => o.CustomerComment).
        //builder.Property(o => o.OriginalSource).
        //builder.Property(o => o.ExternalOrderRef).
        //builder.Property(o => o.Tags).
}
}
