using Crowbond.Modules.OMS.Domain.Orders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.Orders;

internal sealed class OrderDeliveryImageConfiguration : IEntityTypeConfiguration<OrderDeliveryImage>
{
    public void Configure(EntityTypeBuilder<OrderDeliveryImage> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ImageName).IsRequired().HasMaxLength(100);
    }
}
