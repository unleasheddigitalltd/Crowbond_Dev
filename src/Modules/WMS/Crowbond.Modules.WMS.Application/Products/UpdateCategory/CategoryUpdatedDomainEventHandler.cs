using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.IntegrationEvents;

namespace Crowbond.Modules.WMS.Application.Products.UpdateCategory;

internal sealed class CategoryUpdatedDomainEventHandler(IEventBus eventBus)
    : DomainEventHandler<CategoryUpdatedDomainEvent>
{
    public override async Task Handle(
        CategoryUpdatedDomainEvent domainEvent, 
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new CategoryUpdatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.CategoryId,
                domainEvent.Name),
            cancellationToken);
    }
}
