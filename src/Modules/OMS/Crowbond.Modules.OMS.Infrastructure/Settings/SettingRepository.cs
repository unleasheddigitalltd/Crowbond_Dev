using Crowbond.Modules.OMS.Domain.Settings;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.Settings;

public sealed class SettingRepository(OmsDbContext context) : ISettingRepository
{
    public async Task<Setting?> GetAsync(CancellationToken cancellationToken)
    {
        return await context.Settings.SingleOrDefaultAsync(cancellationToken);
    }
}
