namespace Crowbond.Modules.OMS.Application.Orders.GetOrder;

public sealed record OrderItemResponse(
    Guid OrderLineId,
    Guid OrderHeaderId,
    Guid TicketTypeId,
    decimal Quantity,
    decimal UnitPrice,
    decimal Tax,
    decimal LinePrice);
