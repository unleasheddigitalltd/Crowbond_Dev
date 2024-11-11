using Crowbond.Modules.OMS.Domain.Compliances;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.Compliances;

internal sealed class ComplianceLineImageConfiguration : IEntityTypeConfiguration<ComplianceLineImage>
{
    public void Configure(EntityTypeBuilder<ComplianceLineImage> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.ImageName).HasMaxLength(100);
    }
}
