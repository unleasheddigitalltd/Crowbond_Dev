using Crowbond.Modules.CRM.Domain.CustomerOutlets;
using Crowbond.Modules.CRM.Domain.Routes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerOutlets;

internal sealed class CustomerOutletRouteConfiguration : IEntityTypeConfiguration<CustomerOutletRoute>
{
    public void Configure(EntityTypeBuilder<CustomerOutletRoute> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne<Route>().WithMany().HasForeignKey(c => c.RouteId);
    }
}
