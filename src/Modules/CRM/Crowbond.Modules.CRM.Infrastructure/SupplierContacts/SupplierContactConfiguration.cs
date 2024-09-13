using Crowbond.Modules.CRM.Domain.SupplierContacts;
using Crowbond.Modules.CRM.Domain.Suppliers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.SupplierContacts;

internal sealed class SupplierContactConfiguration : IEntityTypeConfiguration<SupplierContact>
{
    public void Configure(EntityTypeBuilder<SupplierContact> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName).IsRequired().HasMaxLength(100);

        builder.Property(c => c.LastName).IsRequired().HasMaxLength(100);

        builder.Property(c => c.Username).IsRequired().HasMaxLength(128);
        builder.HasIndex(t => t.Username).IsUnique();

        builder.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(20);

        builder.Property(c => c.Mobile).HasMaxLength(20);

        builder.Property(c => c.Email).IsRequired().HasMaxLength(255);
        builder.HasIndex(t => t.Email).IsUnique();

        builder.HasOne<Supplier>().WithMany().HasForeignKey(c => c.SupplierId);
    }
}
