using Crowbond.Common.Domain;

namespace Crowbond.Modules.Ticketing.Domain.Orders;

public sealed class OrderTicketsIssuedDomainEvent(Guid orderId) : DomainEvent
{
    public Guid OrderId { get; init; } = orderId;
}
