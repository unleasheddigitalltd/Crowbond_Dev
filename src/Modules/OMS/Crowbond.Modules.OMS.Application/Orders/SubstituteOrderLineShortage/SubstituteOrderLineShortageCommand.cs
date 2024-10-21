using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.SubstituteOrderLineShortage;

public sealed record SubstituteOrderLineShortageCommand(Guid OrderLineId, Guid ProductId): ICommand<Guid>;
