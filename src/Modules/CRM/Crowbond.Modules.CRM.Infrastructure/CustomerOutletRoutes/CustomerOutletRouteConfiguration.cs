using Crowbond.Modules.CRM.Domain.CustomerOutletRoutes;
using Crowbond.Modules.CRM.Domain.CustomerOutlets;
using Crowbond.Modules.CRM.Domain.Routes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerOutletRoutes;

internal sealed class CustomerOutletRouteConfiguration : IEntityTypeConfiguration<CustomerOutletRoute>
{
    public void Configure(EntityTypeBuilder<CustomerOutletRoute> builder)
    {
        builder.HasIndex(c => c.Id);

        builder.Property(c => c.DaysOfWeek).IsRequired().HasColumnType("CHAR(7)");

        builder.HasOne<CustomerOutlet>().WithMany().HasForeignKey(c => c.CustomerOutletId);
        builder.HasOne<Route>().WithMany().HasForeignKey(c => c.RouteId);
    }
}
