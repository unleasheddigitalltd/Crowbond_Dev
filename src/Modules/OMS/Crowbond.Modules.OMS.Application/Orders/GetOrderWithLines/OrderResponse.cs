namespace Crowbond.Modules.OMS.Application.Orders.GetOrderWithLines;

public sealed record OrderResponse(
    Guid Id,
    string OrderNo)
{
    public List<OrderLineResponse> Lines { get; } = [];
}
