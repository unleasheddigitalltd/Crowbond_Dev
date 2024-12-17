using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Drivers.AddDriver;
using Crowbond.Modules.Users.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.OMS.Presentation.Drivers;

internal sealed class DriverRoleAddedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<DriverRoleAddedIntegrationEvent>
{
    public override async Task Handle(
        DriverRoleAddedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new AddDriverCommand(integrationEvent.UserId), cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(AddDriverCommand), result.Error);
        }
    }
}
