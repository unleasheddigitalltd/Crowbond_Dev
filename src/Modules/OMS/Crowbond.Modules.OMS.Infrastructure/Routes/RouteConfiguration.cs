using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Crowbond.Modules.OMS.Domain.Routes;

namespace Crowbond.Modules.OMS.Infrastructure.Routes;

internal sealed class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name).IsRequired().HasMaxLength(100);

        builder.Property(r => r.DaysOfWeek).IsRequired().HasColumnType("CHAR(7)");

    }
}
