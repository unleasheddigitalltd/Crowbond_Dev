using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Crowbond.Modules.OMS.Infrastructure.PurchaseOrders;

public sealed class PurchaseOrderHeaderConfiguratin : IEntityTypeConfiguration<PurchaseOrderHeader>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderHeader> builder)
    {
        var tagsConverter = new ValueConverter<string[], string>(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.None)
            );

        builder.HasKey(p => p.Id);
        builder.Property(p => p.PurchaseOrderNo).HasMaxLength(20);
        builder.Property(p => p.PaidBy).HasMaxLength(100);
        builder.Property(p => p.PaidDate).IsRequired(false);
        builder.Property(p => p.SupplierAccountNumber).HasMaxLength(20);
        builder.Property(p => p.SupplierName).HasMaxLength(100);
        builder.Property(p => p.ContactFullName).HasMaxLength(200);
        builder.Property(p => p.ContactPhone).HasMaxLength(20);
        builder.Property(p => p.ContactEmail).HasMaxLength(255);
        builder.Property(p => p.PurchaseOrderAmount).HasPrecision(10, 2);
        builder.Property(p => p.ShippingLocationName).HasMaxLength(100);
        builder.Property(p => p.ShippingAddressLine1).HasMaxLength(255);
        builder.Property(p => p.ShippingAddressLine2).HasMaxLength(255);
        builder.Property(p => p.ShippingTownCity).HasMaxLength(100);
        builder.Property(p => p.ShippingCounty).HasMaxLength(100);
        builder.Property(p => p.ShippingCountry).HasMaxLength(100);
        builder.Property(p => p.ShippingPostalCode).HasMaxLength(20);
        builder.Property(p => p.ExpectedShippingDate).IsRequired(false);
        builder.Property(p => p.SupplierReference).HasMaxLength(100);
        builder.Property(p => p.PurchaseOrderTax).HasPrecision(10, 2);
        builder.Property(p => p.DeliveryMethod).IsRequired(false);
        builder.Property(p => p.DeliveryCharge).HasPrecision(10, 2);
        builder.Property(p => p.PurchaseOrderNotes).HasMaxLength(500);
        builder.Property(p => p.SalesOrderRef).HasMaxLength(100);
        builder.Property(p => p.Tags).HasConversion(tagsConverter).HasMaxLength(255);
    }
}
