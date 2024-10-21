using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.WMS.IntegrationEvents;

public sealed class CategoryCreatedIntegrationEvent: IntegrationEvent
{
    public CategoryCreatedIntegrationEvent(
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
