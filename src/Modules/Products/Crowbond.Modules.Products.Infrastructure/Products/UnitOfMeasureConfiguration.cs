using Crowbond.Modules.Products.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.Products.Infrastructure.Products;

internal sealed class UnitOfMeasureConfiguration : IEntityTypeConfiguration<UnitOfMeasure>
{
    public void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
    {
        builder.ToTable("unit_of_measures");

        builder.HasKey(c => c.Name);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(20);

        builder.HasData(
            UnitOfMeasure.Bag,
            UnitOfMeasure.Box,
            UnitOfMeasure.Each,
            UnitOfMeasure.Kg,
            UnitOfMeasure.Pack);
    }
}
