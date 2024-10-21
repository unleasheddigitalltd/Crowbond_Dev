using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.WMS.IntegrationEvents;

public sealed class BrandCreatedIntegrationEvent: IntegrationEvent
{
    public BrandCreatedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid brandId,
        string name)
        : base(id, occurredOnUtc)
    {
        BrandId = brandId;
        Name = name;
    }

    public Guid BrandId { get; private set; }
    public string Name { get; private set; }
}
