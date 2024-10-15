using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.OMS.IntegrationEvents;

public sealed class OrderAcceptedIntegrationEvent: IntegrationEvent
{
    public OrderAcceptedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid orderId,
        string orderNo,
        List<OrderLine> lines)
        : base(id, occurredOnUtc)
    {
        OrderId = orderId;
        OrderNo = orderNo;
        Lines = lines;
    }

    public Guid OrderId { get; init; }
    public string OrderNo { get; init; }
    public List<OrderLine> Lines { get; init; }

    public sealed record OrderLine(
        Guid ProductId,
        decimal Qty);
}
