using Crowbond.Modules.CRM.Domain.Reps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.CRM.Infrastructure.Reps;

internal sealed class RepConfiguration : IEntityTypeConfiguration<Rep>
{
    public void Configure(EntityTypeBuilder<Rep> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
    }
}
