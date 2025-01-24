using Crowbond.Modules.OMS.Domain.Drivers;
using Crowbond.Modules.OMS.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crowbond.Modules.OMS.Infrastructure.Drivers;

internal sealed class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.HasKey(d => d.Id);

    }
}
