using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Crowbond.Modules.WMS.Domain.Tasks;
using Crowbond.Modules.WMS.Domain.Receipts;

namespace Crowbond.Modules.WMS.Infrastructure.Tasks;

internal sealed class TaskHeaderConfiguration : IEntityTypeConfiguration<TaskHeader>
{
    public void Configure(EntityTypeBuilder<TaskHeader> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.TaskNo).IsRequired().HasMaxLength(20);

        builder.HasOne<ReceiptHeader>().WithMany().HasForeignKey(t => t.EntityId);
    }
}
