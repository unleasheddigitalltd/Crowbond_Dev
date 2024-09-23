namespace Crowbond.Modules.OMS.Domain.Settings;

public interface ISettingRepository
{
    Task<Setting?> GetAsync(CancellationToken cancellationToken);
}
