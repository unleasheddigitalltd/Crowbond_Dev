using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Products.GetCategory;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.WMS.Application.Products.CreateCategory;

internal sealed class CategoryCreatedDomainEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<CategoryCreatedDomainEvent>
{
    public override async Task Handle(
        CategoryCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<CategoryResponse> result = await sender.Send(
            new GetCategoryQuery(domainEvent.CategoryId), cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(GetCategoryQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new CategoryCreatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Name),
            cancellationToken);
    }
}
