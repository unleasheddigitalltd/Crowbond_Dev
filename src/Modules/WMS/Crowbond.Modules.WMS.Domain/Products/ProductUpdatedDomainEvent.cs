using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Products;

public sealed class ProductUpdatedDomainEvent(Guid productId) : DomainEvent
{
    public Guid ProductId { get; init; } = productId;
}
