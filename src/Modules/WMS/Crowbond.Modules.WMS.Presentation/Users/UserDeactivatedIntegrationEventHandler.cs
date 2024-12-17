using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.IntegrationEvents;
using MediatR;
using Crowbond.Modules.WMS.Application.Users.DeactivateUser;

namespace Crowbond.Modules.WMS.Presentation.Users;

internal sealed class UserDeactivatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserDeactivatedIntegrationEvent>
{
    public override async Task Handle(
        UserDeactivatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new DeactivateUserCommand(
            integrationEvent.UserId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(DeactivateUserCommand), result.Error);
        }
    }
}
