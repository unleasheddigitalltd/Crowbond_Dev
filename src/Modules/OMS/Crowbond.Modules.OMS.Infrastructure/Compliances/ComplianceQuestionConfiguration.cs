using Crowbond.Modules.OMS.Domain.Compliances;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.Compliances;

internal sealed class ComplianceQuestionConfiguration : IEntityTypeConfiguration<ComplianceQuestion>
{
    public void Configure(EntityTypeBuilder<ComplianceQuestion> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Text).HasMaxLength(255);
    }
}
