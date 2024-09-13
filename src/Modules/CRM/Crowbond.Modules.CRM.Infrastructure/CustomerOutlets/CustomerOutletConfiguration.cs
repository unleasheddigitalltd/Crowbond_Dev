using Crowbond.Modules.CRM.Domain.CustomerOutlets;
using Crowbond.Modules.CRM.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerOutlets;

internal sealed class CustomerOutletConfiguration : IEntityTypeConfiguration<CustomerOutlet>
{
    public void Configure(EntityTypeBuilder<CustomerOutlet> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.LocationName).IsRequired().HasMaxLength(100);

        builder.Property(c => c.FullName).IsRequired().HasMaxLength(100);

        builder.Property(c => c.Email).HasMaxLength(255);

        builder.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(20);

        builder.Property(c => c.Mobile).HasMaxLength(20);

        builder.Property(c => c.AddressLine1).IsRequired().HasMaxLength(255);

        builder.Property(c => c.AddressLine2).HasMaxLength(255);

        builder.Property(c => c.TownCity).IsRequired().HasMaxLength(100);

        builder.Property(c => c.County).IsRequired().HasMaxLength(100);

        builder.Property(c => c.Country).HasMaxLength(100);

        builder.Property(c => c.PostalCode).IsRequired().HasMaxLength(16);

        builder.Property(c => c.DeliveryNote).HasMaxLength(500);

        builder.HasOne<Customer>().WithMany().HasForeignKey(c => c.CustomerId);
    }
}
