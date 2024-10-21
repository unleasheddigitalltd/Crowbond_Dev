using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Products;

public sealed class CategoryUpdatedDomainEvent(Guid categoryId, string name) : DomainEvent
{
    public Guid CategoryId { get; init; } = categoryId;
    public string Name { get; init; } = name;
}
