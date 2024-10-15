using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Products.UpdateBrand;
using Crowbond.Modules.WMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.CRM.Presentation.Products;

internal sealed class BrandUpdatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<BrandUpdatedIntegrationEvent>
{
    public override async Task Handle(
        BrandUpdatedIntegrationEvent integrationEvent, 
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateBrandCommand(
                integrationEvent.BrandId,
                integrationEvent.Name),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(UpdateBrandCommand), result.Error);
        }
    }
}
