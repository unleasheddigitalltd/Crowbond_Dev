using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Orders;
public sealed class OrderLineCreatedDomainEvent(Guid customerId, Guid productId): DomainEvent
{
    public Guid CustomerId { get; init; } = customerId;
    public Guid ProductId { get; init; } = productId;
}
