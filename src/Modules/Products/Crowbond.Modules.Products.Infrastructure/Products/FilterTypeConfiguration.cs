using Crowbond.Modules.Products.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.Products.Infrastructure.Products;

internal sealed class FilterTypeConfiguration : IEntityTypeConfiguration<FilterType>
{
    public void Configure(EntityTypeBuilder<FilterType> builder)
    {
        builder.ToTable("filter_types");

        builder.HasKey(c => c.Name);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(20);

        builder.HasData(
            FilterType.Case,
            FilterType.Box,
            FilterType.Each,
            FilterType.Kg,
            FilterType.Processed);

    }
}
