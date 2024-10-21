using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Products.GetBrand;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.WMS.Application.Products.CreateBrand;

internal sealed class BrandCreatedDomainEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<BrandCreatedDomainEvent>
{
    public override async Task Handle(
        BrandCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<BrandResponse> result = await sender.Send(
            new GetBrandQuery(domainEvent.BrandId), cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(GetBrandQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new BrandCreatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Name),
            cancellationToken);
    }
}
