using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Products.UpdateProduct;
using Crowbond.Modules.CRM.Domain.Products;
using Crowbond.Modules.WMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.CRM.Presentation.Products;

internal sealed class ProductUpdatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<ProductUpdatedIntegrationEvent>
{
    public override async Task Handle(
        ProductUpdatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateProductCommand(
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
            throw new CrowbondException(nameof(UpdateProductCommand), result.Error);
        }
    }
}
