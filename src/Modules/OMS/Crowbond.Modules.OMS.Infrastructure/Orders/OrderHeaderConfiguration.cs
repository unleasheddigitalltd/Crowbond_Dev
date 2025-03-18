using Crowbond.Modules.OMS.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.Orders;

internal sealed class OrderHeaderConfiguration : IEntityTypeConfiguration<OrderHeader>
{
    public void Configure(EntityTypeBuilder<OrderHeader> builder)
    {
        builder.ToTable("OrderHeaders", "oms");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrderNo)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.PurchaseOrderNo)
            .HasMaxLength(50);

        builder.Property(x => x.CustomerId)
            .IsRequired();

        builder.Property(x => x.CustomerOutletId)
            .IsRequired();

        builder.Property(x => x.CustomerAccountNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.CustomerBusinessName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.DeliveryLocationName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.DeliveryFullName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.DeliveryEmail)
            .HasMaxLength(200);

        builder.Property(x => x.DeliveryPhone)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.DeliveryMobile)
            .HasMaxLength(50);

        builder.Property(x => x.DeliveryNotes)
            .HasMaxLength(500);

        builder.Property(x => x.DeliveryAddressLine1)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.DeliveryAddressLine2)
            .HasMaxLength(200);

        builder.Property(x => x.DeliveryTownCity)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.DeliveryCounty)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.DeliveryCountry)
            .HasMaxLength(100);

        builder.Property(x => x.DeliveryPostalCode)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.ShippingDate)
            .IsRequired();

        builder.Property(x => x.RouteTripId);

        builder.Property(x => x.RouteName)
            .HasMaxLength(200);

        builder.Property(x => x.DeliveryMethod)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.DeliveryCharge)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.OrderAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.OrderTax)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.PaymentStatus)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.DueDateCalculationBasis)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.DueDaysForInvoice)
            .IsRequired();

        builder.Property(x => x.PaymentMethod)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.PaymentDueDate);

        builder.Property(x => x.CustomerComment)
            .HasMaxLength(500);

        builder.Property(x => x.OriginalSource)
            .HasMaxLength(50);

        builder.Property(x => x.ExternalOrderRef)
            .HasMaxLength(50);

        builder.Property(x => x.Tags)
            .HasMaxLength(500);

        builder.Property(x => x.LastImageSequence)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .IsRequired();

        builder.Property(x => x.CreatedOnUtc)
            .IsRequired();

        builder.Property(x => x.LastModifiedBy);

        builder.Property(x => x.LastModifiedOnUtc);

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(x => x.DeletedBy);

        builder.Property(x => x.DeletedOnUtc);

        builder.HasMany(x => x.Lines)
            .WithOne()
            .HasForeignKey("OrderHeaderId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Delivery)
            .WithOne()
            .HasForeignKey("OrderHeaderId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
