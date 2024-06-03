using Crowbond.Common.Domain;

namespace Crowbond.Modules.Products.Domain.Products;

public sealed class ProductCreatedDomainEvent(Guid productId) : DomainEvent
{
    public Guid ProductId { get; init; } = productId;
}
