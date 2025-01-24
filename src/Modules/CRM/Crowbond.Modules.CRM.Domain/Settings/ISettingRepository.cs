namespace Crowbond.Modules.CRM.Domain.Settings;

public interface ISettingRepository
{
    Task<Setting?> GetAsync(CancellationToken cancellationToken);

    void Insert(Setting setting);

    void Remove(Setting setting);
}
