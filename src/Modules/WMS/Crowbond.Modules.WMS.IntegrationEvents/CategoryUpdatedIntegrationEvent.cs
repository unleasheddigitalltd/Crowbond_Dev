using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.WMS.IntegrationEvents;

public sealed class CategoryUpdatedIntegrationEvent: IntegrationEvent
{
    public CategoryUpdatedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid categoryId,
        string name)
        : base(id, occurredOnUtc)
    {
        CategoryId = categoryId;
        Name = name;
    }

    public Guid CategoryId { get; private set; }
    public string Name { get; private set; }
}
