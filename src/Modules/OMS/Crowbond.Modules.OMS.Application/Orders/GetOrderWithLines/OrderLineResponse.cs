namespace Crowbond.Modules.OMS.Application.Orders.GetOrderWithLines;

public sealed record OrderLineResponse(Guid OrderLineId, Guid OrderHeaderId, Guid ProductId, decimal Qty);
