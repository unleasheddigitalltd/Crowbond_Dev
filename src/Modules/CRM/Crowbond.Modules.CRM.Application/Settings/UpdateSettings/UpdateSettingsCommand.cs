using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.CRM.Domain.Settings;

namespace Crowbond.Modules.CRM.Application.Settings.UpdateSettings;

public sealed record UpdateSettingsCommand(PaymentTerms? PaymentTerms) : ICommand;
