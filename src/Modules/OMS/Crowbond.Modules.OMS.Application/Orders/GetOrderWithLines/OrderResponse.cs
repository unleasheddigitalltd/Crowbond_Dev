namespace Crowbond.Modules.OMS.Application.Orders.GetOrderWithLines;

public sealed record OrderResponse(
    Guid Id,
    Guid RouteTripId,
    string OrderNo,
    string CustomerBusinessName)
{
    public List<OrderLineResponse> Lines { get; } = [];
}
