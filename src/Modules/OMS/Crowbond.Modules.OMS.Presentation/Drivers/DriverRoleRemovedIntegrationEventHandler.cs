using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Drivers.RemoveDriver;
using Crowbond.Modules.Users.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.OMS.Presentation.Drivers;

internal sealed class DriverRoleRemovedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<DriverRoleRemovedIntegrationEvent>
{
    public override async Task Handle(
        DriverRoleRemovedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new RemoveDriverCommand(integrationEvent.UserId), cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(RemoveDriverCommand), result.Error);
        }
    }
}
