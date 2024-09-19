using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Domain.RouteTrips;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.Orders;

public sealed class OrderHeaderConfiguratin : IEntityTypeConfiguration<OrderHeader>
{
    public void Configure(EntityTypeBuilder<OrderHeader> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.OrderNumber).HasMaxLength(100);
        builder.Property(o => o.PurchaseOrderNumber).HasMaxLength(100);
        builder.Property(o => o.CustomerBusinessName).HasMaxLength(100);
        builder.Property(o => o.DeliveryLocationName).HasMaxLength(100);
        builder.Property(o => o.DeliveryFullName).HasMaxLength(100);
        builder.Property(o => o.DeliveryEmail).HasMaxLength(255);
        builder.Property(o => o.DeliveryPhone).HasMaxLength(20);
        builder.Property(o => o.DeliveryMobile).HasMaxLength(20);
        builder.Property(o => o.DeliveryNotes).HasMaxLength(500);
        builder.Property(o => o.DeliveryAddressLine1).HasMaxLength(255);
        builder.Property(o => o.DeliveryAddressLine2).HasMaxLength(255);
        builder.Property(o => o.DeliveryTownCity).HasMaxLength(100);
        builder.Property(o => o.DeliveryCounty).HasMaxLength(100);
        builder.Property(o => o.DeliveryCountry).HasMaxLength(100);
        builder.Property(o => o.DeliveryPostalCode).HasMaxLength(20);
        builder.Property(o => o.DeliveryCharge).HasPrecision(10, 2);
        builder.Property(o => o.OrderAmount).HasPrecision(10, 2);
        builder.Property(o => o.OrderTax).HasPrecision(10, 2);
        builder.Property(o => o.CustomerComment).HasMaxLength(3000);
        builder.Property(o => o.OriginalSource).HasMaxLength(100);
        builder.Property(o => o.ExternalOrderRef).HasMaxLength(100);
        builder.Property(o => o.Tags).HasMaxLength(255);

        builder.HasOne<RouteTrip>().WithMany().HasForeignKey(o => o.RouteTripId).OnDelete(DeleteBehavior.NoAction);
    }
}
