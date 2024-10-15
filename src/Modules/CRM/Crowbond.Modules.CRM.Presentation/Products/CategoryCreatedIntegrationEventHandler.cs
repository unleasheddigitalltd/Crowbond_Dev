using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Products.CreateCategory;
using Crowbond.Modules.WMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.CRM.Presentation.Products;

internal sealed class CategoryCreatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<CategoryCreatedIntegrationEvent>
{
    public override async Task Handle(
        CategoryCreatedIntegrationEvent integrationEvent, 
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateCategoryCommand(
                integrationEvent.CategoryId,
                integrationEvent.Name),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateCategoryCommand), result.Error);
        }
    }
}
