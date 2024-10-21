using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.IntegrationEvents;

namespace Crowbond.Modules.WMS.Application.Products.UpdateBrand;

internal sealed class BrandUpdatedDomainEventHandler(IEventBus eventBus)
    : DomainEventHandler<BrandUpdatedDomainEvent>
{
    public override async Task Handle(
        BrandUpdatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new BrandUpdatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.Id,
                domainEvent.Name),
            cancellationToken);
    }
}
