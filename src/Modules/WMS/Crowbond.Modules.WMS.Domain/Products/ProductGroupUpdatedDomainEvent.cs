using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Products;

public sealed class ProductGroupUpdatedDomainEvent(Guid productGroupId, string name) : DomainEvent
{
    public Guid ProductGroupId { get; init; } = productGroupId;
    public string Name { get; init; } = name;
}
