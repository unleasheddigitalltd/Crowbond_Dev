using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.WMS.IntegrationEvents;

public sealed class ProductGroupCreatedIntegrationEvent: IntegrationEvent
{
    public ProductGroupCreatedIntegrationEvent(
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
