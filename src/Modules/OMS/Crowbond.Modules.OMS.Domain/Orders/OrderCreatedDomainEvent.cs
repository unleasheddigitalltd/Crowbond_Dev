using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Orders;
public sealed class OrderCreatedDomainEvent(Guid orderId) : DomainEvent
{
    public Guid OrderId { get; init; } = orderId;
}
