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

        builder.Property(c => c.AccountNumber).IsRequired().HasMaxLength(16);

        builder.Property(c => c.BusinessName).IsRequired().HasMaxLength(100);

        builder.Property(c => c.BillingAddressLine1).IsRequired().HasMaxLength(255);

        builder.Property(c => c.BillingAddressLine2).HasMaxLength(255);

        builder.Property(c => c.BillingTownCity).IsRequired().HasMaxLength(100);

        builder.Property(c => c.BillingCounty).IsRequired().HasMaxLength(100);

        builder.Property(c => c.BillingCountry).IsRequired(false).HasMaxLength(100);

        builder.Property(c => c.BillingPostalCode).IsRequired().HasMaxLength(16);

        builder.Property(c => c.Discount).IsRequired().HasPrecision(5, 2);

        builder.Property(c => c.PaymentTerms).IsRequired(false);

        builder.Property(c => c.InvoiceDueDays).IsRequired(false);

        builder.Property(c => c.DeliveryMinOrderValue).IsRequired(false).HasPrecision(10, 2);

        builder.Property(c => c.DeliveryCharge).IsRequired(false).HasPrecision(10, 2);

        builder.Property(c => c.CustomerNotes).HasMaxLength(500);

        builder.HasOne<Rep>().WithMany().HasForeignKey(c => c.RepId).OnDelete(DeleteBehavior.NoAction);
    }
}
