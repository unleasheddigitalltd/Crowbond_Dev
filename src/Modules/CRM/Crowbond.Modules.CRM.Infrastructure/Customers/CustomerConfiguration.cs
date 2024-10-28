using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.Reps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.Customers;
internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.AccountNumber).HasMaxLength(16);
        builder.Property(c => c.BusinessName).HasMaxLength(100);
        builder.Property(c => c.BillingAddressLine1).HasMaxLength(255);
        builder.Property(c => c.BillingAddressLine2).HasMaxLength(255);
        builder.Property(c => c.BillingTownCity).HasMaxLength(100);
        builder.Property(c => c.BillingCounty).HasMaxLength(100);
        builder.Property(c => c.BillingCountry).HasMaxLength(100);
        builder.Property(c => c.BillingPostalCode).HasMaxLength(16);
        builder.Property(c => c.Discount).HasPrecision(5, 2);
        builder.Property(c => c.DeliveryMinOrderValue).HasPrecision(10, 2);
        builder.Property(c => c.DeliveryCharge).HasPrecision(10, 2);
        builder.Property(c => c.CustomerNotes).HasMaxLength(500);

        builder.HasOne<Rep>().WithMany().HasForeignKey(c => c.RepId).OnDelete(DeleteBehavior.NoAction);
    }
}
