using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Settings.GetSettings;

public sealed record GetSettingsQuery() : IQuery<SettingsResponse>;
