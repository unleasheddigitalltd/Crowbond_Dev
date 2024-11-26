namespace Crowbond.Modules.OMS.Application.Orders.DeliverOrderLine;

public sealed record OrderLineRequest(decimal DeliveredQty)
{
    public List<OrderLineRejectRequest> Rejects { get; init; }
}
