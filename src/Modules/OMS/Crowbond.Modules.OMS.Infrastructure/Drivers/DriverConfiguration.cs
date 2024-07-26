using Crowbond.Modules.OMS.Domain.Drivers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.Drivers;

internal sealed class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(d => d.LastName).IsRequired().HasMaxLength(100);
        builder.Property(d => d.Username).IsRequired().HasMaxLength(100);
        builder.Property(d => d.Email).IsRequired().HasMaxLength(255);
        builder.Property(d => d.Mobile).IsRequired().HasMaxLength(20);
        builder.Property(d => d.VehicleRegn).HasMaxLength(10);

        builder.HasIndex(d => d.Username).IsUnique();
        builder.HasIndex(d => d.Email).IsUnique();
    }
}
