using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Products.UpdateProductGroup;
using Crowbond.Modules.WMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.CRM.Presentation.Products;

internal sealed class ProductGroupUpdatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<ProductGroupUpdatedIntegrationEvent>
{
    public override async Task Handle(
        ProductGroupUpdatedIntegrationEvent integrationEvent, 
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateProductGroupCommand(
                integrationEvent.ProductGroupId,
                integrationEvent.Name),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(UpdateProductGroupCommand), result.Error);
        }
    }
}
