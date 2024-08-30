using Crowbond.Modules.WMS.Domain.WarehouseOperators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.WarehouseOperators;

internal sealed class WarehouseOperatorConfiguration : IEntityTypeConfiguration<WarehouseOperator>
{
    public void Configure(EntityTypeBuilder<WarehouseOperator> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(d => d.LastName).IsRequired().HasMaxLength(100);
        builder.Property(d => d.Username).IsRequired().HasMaxLength(100);
        builder.Property(d => d.Email).IsRequired().HasMaxLength(255);
        builder.Property(d => d.Mobile).IsRequired().HasMaxLength(20);

        builder.HasIndex(d => d.Username).IsUnique();
        builder.HasIndex(d => d.Email).IsUnique();
    }
}
