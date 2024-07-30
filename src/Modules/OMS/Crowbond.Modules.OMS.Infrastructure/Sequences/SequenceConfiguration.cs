using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Crowbond.Modules.OMS.Domain.Sequences;

namespace Crowbond.Modules.OMS.Infrastructure.Sequences;

internal sealed class SequenceConfiguration : IEntityTypeConfiguration<Sequence>
{
    public void Configure(EntityTypeBuilder<Sequence> builder)
    {
        builder.HasKey(s => s.Id);
    }
}
