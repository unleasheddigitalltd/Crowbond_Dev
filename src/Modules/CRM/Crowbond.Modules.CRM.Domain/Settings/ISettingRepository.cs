namespace Crowbond.Modules.CRM.Domain.Settings;

public interface ISettingRepository
{
    Task<Setting?> GetAsync(CancellationToken cancellationToken);
}
