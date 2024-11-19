using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.AddOrderLine;

public sealed record AddOrderLineCommand(Guid OrderHeaderId, Guid ProductId, decimal Qty) : ICommand<Guid>;
