using Crowbond.Modules.OMS.Domain.Compliances;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.Compliances;

internal sealed class ComplianceLineConfiguration : IEntityTypeConfiguration<ComplianceLine>
{
    public void Configure(EntityTypeBuilder<ComplianceLine> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Description).HasMaxLength(255);

        builder.HasOne<ComplianceQuestion>().WithMany().HasForeignKey(c => c.ComplianceQuestionId);
    }
}
