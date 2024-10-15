using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Products.CreateBrand;
using Crowbond.Modules.WMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.CRM.Presentation.Products;

internal sealed class BrandCreatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<BrandCreatedIntegrationEvent>
{
    public override async Task Handle(
        BrandCreatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateBrandCommand(
                integrationEvent.BrandId,
                integrationEvent.Name),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateBrandCommand), result.Error);
        }
    }
}
