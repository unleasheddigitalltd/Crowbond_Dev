using Crowbond.Modules.OMS.Domain.Deliveries;
using Crowbond.Modules.OMS.Domain.RouteTripLogDatails;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.Deliveries;

internal sealed class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Comments).HasMaxLength(255);

        builder.HasOne<RouteTripLogDatail>().WithMany().HasForeignKey(x => x.RouteTripLogDetailId).OnDelete(DeleteBehavior.NoAction);
    }
}
