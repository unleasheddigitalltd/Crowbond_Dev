using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.OMS.IntegrationEvents;

public sealed class OrderAcceptedIntegrationEvent: IntegrationEvent
{
    public OrderAcceptedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid routeTripId,
        Guid orderId,
        string orderNo,
        string customerBusinessName,
        List<OrderLine> lines)
        : base(id, occurredOnUtc)
    {
        RouteTripId = routeTripId;
        OrderId = orderId;
        OrderNo = orderNo;
        CustomerBusinessName = customerBusinessName;
        Lines = lines;
    }

    public Guid RouteTripId { get; init; }
    public Guid OrderId { get; init; }
    public string OrderNo { get; init; }
    public string CustomerBusinessName { get; init; }
    public List<OrderLine> Lines { get; init; }

    public sealed record OrderLine(
        Guid OrderLineId,
        Guid ProductId,
        decimal Qty);
}
