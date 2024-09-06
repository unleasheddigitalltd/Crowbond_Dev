using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Products.CreateProduct;
using Crowbond.Modules.CRM.Domain.Products;
using Crowbond.Modules.WMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.CRM.Presentation.Products;

internal sealed class ProductCreatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<ProductCreatedIntegrationEvent>
{
    public override async Task Handle(
        ProductCreatedIntegrationEvent integrationEvent, 
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateProductCommand(
                integrationEvent.ProductId,
                integrationEvent.Name,
                integrationEvent.Sku,
                integrationEvent.FilterTypeName,
                integrationEvent.UnitOfMeasureName,
                integrationEvent.InventoryTypeName,
                integrationEvent.CategoryId,
                integrationEvent.CategoryName,
                (TaxRateType)integrationEvent.TaxRateType,
                integrationEvent.IsActive),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateProductCommand), result.Error);
        }
    }
}
