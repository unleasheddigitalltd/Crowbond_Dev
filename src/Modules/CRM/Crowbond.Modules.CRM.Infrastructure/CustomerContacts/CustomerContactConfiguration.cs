using Crowbond.Modules.CRM.Domain.CustomerContacts;
using Crowbond.Modules.CRM.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerContacts;

internal sealed class CustomerContactConfiguration : IEntityTypeConfiguration<CustomerContact>
{
    public void Configure(EntityTypeBuilder<CustomerContact> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName).IsRequired().HasMaxLength(100);

        builder.Property(c => c.LastName).IsRequired().HasMaxLength(100);

        builder.Property(c => c.Username).IsRequired().HasMaxLength(128);
        builder.HasIndex(t => t.Username).IsUnique();

        builder.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(20);

        builder.Property(c => c.Mobile).IsRequired().HasMaxLength(20);

        builder.Property(c => c.Email).IsRequired().HasMaxLength(255);
        builder.HasIndex(t => t.Email).IsUnique();

        builder.HasOne<Customer>().WithMany().HasForeignKey(c => c.CustomerId);
    }
}
