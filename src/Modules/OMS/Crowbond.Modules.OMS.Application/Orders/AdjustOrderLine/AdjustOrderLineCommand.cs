using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.AdjustOrderLine;

public sealed record AdjustOrderLineCommand(Guid OrderLineId): ICommand;
