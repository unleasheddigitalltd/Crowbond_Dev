using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Products.CreateProductGroup;
using Crowbond.Modules.WMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.CRM.Presentation.Products;

internal sealed class ProductGroupCreatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<ProductGroupCreatedIntegrationEvent>
{
    public override async Task Handle(
        ProductGroupCreatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateProductGroupCommand(
                integrationEvent.ProductGroupId,
                integrationEvent.Name),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateProductGroupCommand), result.Error);
        }
    }
}
