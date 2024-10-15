using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Products.UpdateCategory;
using Crowbond.Modules.WMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.CRM.Presentation.Products;

internal sealed class CategoryUpdatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<CategoryUpdatedIntegrationEvent>
{
    public override async Task Handle(
        CategoryUpdatedIntegrationEvent integrationEvent, 
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateCategoryCommand(
                integrationEvent.CategoryId,
                integrationEvent.Name),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(UpdateCategoryCommand), result.Error);
        }
    }
}
