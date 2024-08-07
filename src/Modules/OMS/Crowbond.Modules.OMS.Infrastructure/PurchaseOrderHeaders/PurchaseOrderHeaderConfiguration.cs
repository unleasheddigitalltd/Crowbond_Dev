using Crowbond.Modules.OMS.Domain.PurchaseOrderHeaders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Crowbond.Modules.OMS.Infrastructure.PurchaseOrderHeaders;

public sealed class PurchaseOrderHeaderConfiguratin : IEntityTypeConfiguration<PurchaseOrderHeader>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderHeader> builder)
    {
        var tagsConverter = new ValueConverter<string[], string>(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.None)
            );

        builder.HasKey(p => p.Id);
        builder.Property(p => p.PurchaseOrderNo).IsRequired().HasMaxLength(20);
        builder.Property(p => p.PaidBy).HasMaxLength(100);
        builder.Property(p => p.PaidDate).IsRequired(false);
        builder.Property(p => p.SupplierName).IsRequired().HasMaxLength(100);
        builder.Property(p => p.ContactFullName).IsRequired(false).HasMaxLength(200);
        builder.Property(p => p.ContactPhone).IsRequired(false).HasMaxLength(20);
        builder.Property(p => p.ContactEmail).IsRequired(false).HasMaxLength(255);
        builder.Property(p => p.PurchaseOrderAmount).IsRequired().HasPrecision(10, 2);
        builder.Property(p => p.ShippingLocationName).IsRequired().HasMaxLength(100);
        builder.Property(p => p.ShippingAddressLine1).IsRequired().HasMaxLength(255);
        builder.Property(p => p.ShippingAddressLine2).HasMaxLength(255);
        builder.Property(p => p.ShippingTownCity).IsRequired().HasMaxLength(100);
        builder.Property(p => p.ShippingCounty).IsRequired().HasMaxLength(100);
        builder.Property(p => p.ShippingCountry).IsRequired(false).HasMaxLength(100);
        builder.Property(p => p.ShippingPostalCode).IsRequired().HasMaxLength(20);
        builder.Property(p => p.SupplierReference).HasMaxLength(100);
        builder.Property(p => p.PurchaseOrderTax).IsRequired().HasPrecision(10, 2);
        builder.Property(p => p.DeliveryMethod).IsRequired(false);
        builder.Property(p => p.DeliveryCharge).IsRequired().HasPrecision(10, 2);
        builder.Property(p => p.PurchaseOrderNotes).HasMaxLength(500);
        builder.Property(p => p.SalesOrderRef).HasMaxLength(100);
        builder.Property(p => p.Tags).HasConversion(tagsConverter).HasMaxLength(255);
        
    }
}
