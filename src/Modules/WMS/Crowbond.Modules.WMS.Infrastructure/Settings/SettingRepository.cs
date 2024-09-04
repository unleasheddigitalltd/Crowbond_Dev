using Crowbond.Modules.WMS.Domain.Settings;
using Crowbond.Modules.WMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.WMS.Infrastructure.Settings;

public sealed class SettingRepository(WmsDbContext context) : ISettingRepository
{
    public async Task<Setting?> GetAsync(CancellationToken cancellationToken)
    {
        return await context.Settings.SingleOrDefaultAsync(cancellationToken);
    }
}
