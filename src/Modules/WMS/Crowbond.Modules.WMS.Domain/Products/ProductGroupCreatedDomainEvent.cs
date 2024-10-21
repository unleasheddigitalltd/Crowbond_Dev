using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Products;

public sealed class ProductGroupCreatedDomainEvent(Guid productGroupId) : DomainEvent
{
    public Guid ProductGroupId { get; init; } = productGroupId;
}
