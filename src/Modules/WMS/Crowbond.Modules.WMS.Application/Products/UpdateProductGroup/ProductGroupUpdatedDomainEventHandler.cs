using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.IntegrationEvents;

namespace Crowbond.Modules.WMS.Application.Products.UpdateProductGroup;

internal sealed class ProductGroupUpdatedDomainEventHandler(IEventBus eventBus)
    : DomainEventHandler<ProductGroupUpdatedDomainEvent>
{
    public override async Task Handle(
        ProductGroupUpdatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new ProductGroupUpdatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.ProductGroupId,
                domainEvent.Name), 
            cancellationToken);
    }
}
