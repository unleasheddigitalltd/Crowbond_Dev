namespace Crowbond.Modules.OMS.Application.Orders.GetOrderWithLines;

public sealed record OrderResponse()
{
    public Guid Id { get; }
    public Guid RouteTripId { get; }
    public string OrderNo { get; }
    public string CustomerBusinessName { get; }
    public List<OrderLineResponse> Lines { get; } = [];
}
