using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.WMS.IntegrationEvents;

public sealed class ProductGroupUpdatedIntegrationEvent: IntegrationEvent
{
    public ProductGroupUpdatedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid productGroupId,
        string name)
        : base(id, occurredOnUtc)
    {
        ProductGroupId = productGroupId;
        Name = name;
    }

    public Guid ProductGroupId { get; private set; }
    public string Name { get; private set; }
}
