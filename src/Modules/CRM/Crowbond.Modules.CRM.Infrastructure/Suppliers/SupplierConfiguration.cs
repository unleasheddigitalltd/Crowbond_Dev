using Crowbond.Modules.CRM.Domain.Suppliers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.Suppliers;
internal sealed class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("suppliers");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.SupplierName).IsRequired().HasMaxLength(100);
        builder.Property(s => s.AccountNumber).IsRequired().HasMaxLength(16);
        builder.Property(s => s.AddressLine1).IsRequired().HasMaxLength(255);
        builder.Property(s => s.AddressLine2).HasMaxLength(255);
        builder.Property(s => s.TownCity).IsRequired().HasMaxLength(100);
        builder.Property(s => s.County).IsRequired().HasMaxLength(100);
        builder.Property(s => s.Country).HasMaxLength(100);
        builder.Property(s => s.PostalCode).IsRequired().HasMaxLength(20);
        builder.Property(s => s.SupplierNotes).HasMaxLength(500);
    }
}
