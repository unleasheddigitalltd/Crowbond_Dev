using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.IntegrationEvents;
using Crowbond.Modules.WMS.Application.WarehouseOperators.AddWarehouseOperator;
using MediatR;

namespace Crowbond.Modules.WMS.Presentation.WarehouseOperators;

internal sealed class WarehouseOperatorRoleAddedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<WarehouseOperatorRoleAddedIntegrationEvent>
{
    public override async Task Handle(
        WarehouseOperatorRoleAddedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new AddWarehouseOperatorCommand(integrationEvent.UserId), cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(AddWarehouseOperatorCommand), result.Error);
        }
    }
}
