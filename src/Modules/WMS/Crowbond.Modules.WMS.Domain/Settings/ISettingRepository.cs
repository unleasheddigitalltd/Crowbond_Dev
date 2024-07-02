namespace Crowbond.Modules.WMS.Domain.Settings;

public interface ISettingRepository
{
    Task<Setting?> GetAsync(CancellationToken cancellationToken);
}
