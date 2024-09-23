using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Crowbond.Modules.OMS.Domain.Settings;

namespace Crowbond.Modules.OMS.Infrastructure.Settings;

public sealed class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder.HasKey(s => s.Id);
        builder.HasData(Setting.Initial);
    }
}
