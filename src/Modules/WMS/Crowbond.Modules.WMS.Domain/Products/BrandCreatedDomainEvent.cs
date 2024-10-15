using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Products;

public sealed class BrandCreatedDomainEvent(Guid brandId) : DomainEvent
{
    public Guid BrandId { get; init; } = brandId;
}
