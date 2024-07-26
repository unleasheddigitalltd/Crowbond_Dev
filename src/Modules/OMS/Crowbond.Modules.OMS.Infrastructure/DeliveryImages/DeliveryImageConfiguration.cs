using Crowbond.Modules.OMS.Domain.Deliveries;
using Crowbond.Modules.OMS.Domain.DeliveryImages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.DeliveryImages;

internal sealed class DeliveryImageConfiguration : IEntityTypeConfiguration<DeliveryImage>
{
    public void Configure(EntityTypeBuilder<DeliveryImage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ImageId).IsRequired().HasMaxLength(100);

        builder.HasOne<Delivery>().WithMany().HasForeignKey(x => x.DeliveryId);
    }
}
