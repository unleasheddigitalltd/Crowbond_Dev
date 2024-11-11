using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Settings;

public static class SettingErrors
{
    public static readonly Error NotFound = Error.NotFound("Settings.NotFound", $"The setting was not found");
}
