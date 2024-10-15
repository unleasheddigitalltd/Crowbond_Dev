using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Products;

public sealed class BrandUpdatedDomainEvent(Guid brandId, string name) : DomainEvent
{
    public Guid BrandId { get; init; } = brandId;
    public string Name { get; init; } = name;
}
