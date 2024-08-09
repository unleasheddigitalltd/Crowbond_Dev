using Crowbond.Modules.OMS.Domain.PurchaseOrderHeaders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.PurchaseOrderHeaders;

internal sealed class PurchaseOrderStatusHistoryConfiguration : IEntityTypeConfiguration<PurchaseOrderStatusHistory>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderStatusHistory> builder)
    {
        builder.HasKey(o => o.Id);

        builder.HasOne<PurchaseOrderHeader>().WithMany().HasForeignKey(o => o.PurchaseOrderHeaderId);
    }
}
