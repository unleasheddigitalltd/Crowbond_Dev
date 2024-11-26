namespace Crowbond.Modules.OMS.Application.Orders.DeliverOrderLine;

public sealed record OrderLineRejectRequest(decimal RejectQty, Guid RejectReasonId, string? Comments);
