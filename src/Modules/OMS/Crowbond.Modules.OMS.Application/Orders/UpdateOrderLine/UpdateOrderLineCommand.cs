using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.UpdateOrderLine;

public sealed record UpdateOrderLineCommand(Guid OrderLineId, decimal Qty) : ICommand;
