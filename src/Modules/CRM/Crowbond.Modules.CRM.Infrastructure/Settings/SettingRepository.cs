using Crowbond.Modules.CRM.Domain.Settings;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.Settings;

public sealed class SettingRepository(CrmDbContext context) : ISettingRepository
{
    public async Task<Setting?> GetAsync(CancellationToken cancellationToken)
    {
        return await context.Settings.SingleOrDefaultAsync(cancellationToken);
    }

    public void Insert(Setting setting)
    {
        context.Settings.Add(setting);
    }

    public void Remove(Setting setting)
    {
        context.Settings.Remove(setting);
    }
}
