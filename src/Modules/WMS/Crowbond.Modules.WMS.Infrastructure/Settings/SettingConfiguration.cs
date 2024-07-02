using Crowbond.Modules.WMS.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.WMS.Infrastructure.Settings;

public sealed class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasData(new Setting()
        {
            Id = Guid.NewGuid(),
            HasMixBatchLocation = false,
            CreatedDate = DateTime.UtcNow,
            IsActive = true,
        });
    }
}
