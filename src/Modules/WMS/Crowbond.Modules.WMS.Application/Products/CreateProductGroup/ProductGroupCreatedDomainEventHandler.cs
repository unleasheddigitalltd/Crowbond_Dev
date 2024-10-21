using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Products.GetProductGroup;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.WMS.Application.Products.CreateProductGroup;

internal sealed class ProductGroupCreatedDomainEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<ProductGroupCreatedDomainEvent>
{
    public override async Task Handle(
        ProductGroupCreatedDomainEvent domainEvent, 
        CancellationToken cancellationToken = default)
    {
        Result<ProductGroupResponse> result = await sender.Send(
            new GetProductGroupQuery(domainEvent.ProductGroupId), cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(GetProductGroupQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new ProductGroupCreatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Name),
            cancellationToken);
    }
}
