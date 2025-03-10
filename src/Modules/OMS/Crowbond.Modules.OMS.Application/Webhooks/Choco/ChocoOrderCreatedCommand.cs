using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.OMS.Domain.Webhooks.Choco;

namespace Crowbond.Modules.OMS.Application.Webhooks.Choco;

public sealed record ChocoOrderCreatedCommand(ChocoOrderCreatedWebhook webhook) : ICommand;
