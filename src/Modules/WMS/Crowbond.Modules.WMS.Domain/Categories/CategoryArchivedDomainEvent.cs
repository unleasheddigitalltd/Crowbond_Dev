using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Categories;
public sealed class CategoryArchivedDomainEvent(Guid categoryId) : DomainEvent
{
    public Guid CategoryId { get; init; } = categoryId;
}
