using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.IntegrationEvents;
using Crowbond.Modules.WMS.Application.WarehouseOperators.RemoveWarehouseOperator;
using MediatR;

namespace Crowbond.Modules.WMS.Presentation.WarehouseOperators;

internal sealed class WarehouseOperatorRoleRemovedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<WarehouseOperatorRoleRemovedIntegrationEvent>
{
    public override async Task Handle(
        WarehouseOperatorRoleRemovedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new RemoveWarehouseOperatorCommand(integrationEvent.UserId), cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(RemoveWarehouseOperatorCommand), result.Error);
        }
    }
}
