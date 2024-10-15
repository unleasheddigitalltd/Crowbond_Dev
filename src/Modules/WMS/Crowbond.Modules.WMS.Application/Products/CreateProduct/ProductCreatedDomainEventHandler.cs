using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Products.GetProductDetails;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.WMS.Application.Products.CreateProduct;

internal sealed class ProductCreatedDomainEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<ProductCreatedDomainEvent>
{
    public override async Task Handle(
        ProductCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<ProductDetailsResponse> result = await sender.Send(
            new GetProductDetailsQuery(domainEvent.ProductId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(GetProductDetailsQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new ProductCreatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Name,
                result.Value.Sku,
                result.Value.FilterTypeName,
                result.Value.UnitOfMeasureName,
                result.Value.InventoryTypeName,
                result.Value.CategoryId,
                result.Value.BrandId,
                result.Value.ProductGroupId,
                result.Value.TaxRateType,
                result.Value.IsActive),
            cancellationToken);
    }
}
