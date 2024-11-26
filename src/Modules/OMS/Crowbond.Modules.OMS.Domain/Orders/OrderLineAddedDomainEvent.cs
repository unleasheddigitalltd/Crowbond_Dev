using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Orders;

public sealed class OrderLineAddedDomainEvent(Guid customerId, Guid productId): DomainEvent
{
    public Guid CustomerId { get; init; } = customerId;
    public Guid ProductId { get; init; } = productId;
}
