using Crowbond.Modules.OMS.Domain.Compliances;
using Crowbond.Modules.OMS.Domain.RouteTripLogs;
using Crowbond.Modules.OMS.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.Compliances;

internal sealed class ComplianceHeaderConfiguration : IEntityTypeConfiguration<ComplianceHeader>
{
    public void Configure(EntityTypeBuilder<ComplianceHeader> builder)
    {
        builder.HasKey(c =>  c.Id);
        builder.Property(c => c.FormNo).HasMaxLength(20);
        builder.Property(x => x.Temperature).HasPrecision(2, 2);

        builder.HasOne<RouteTripLog>().WithMany().HasForeignKey(c => c.RouteTripLogId);
        builder.HasOne<Vehicle>().WithMany().HasForeignKey(c => c.VehicleId);
    }
}
