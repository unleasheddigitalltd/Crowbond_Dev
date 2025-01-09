using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.CRM.Domain.Settings;

namespace Crowbond.Modules.CRM.Application.Settings.GetSettings;

public sealed record SettingsResponse(PaymentTerms PaymentTerms);
