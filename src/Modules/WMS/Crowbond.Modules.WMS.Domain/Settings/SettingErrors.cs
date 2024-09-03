using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Settings;

public static class SettingErrors
{
    public static readonly Error NotFound = Error.NotFound("Settings.NotFound", $"The setting was not found");
}
