using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Users.UpdateUser;
using Crowbond.Modules.Users.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.CRM.Presentation.Users;

internal sealed class UserProfileUpdatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserProfileUpdatedIntegrationEvent>
{
    public override async Task Handle(
        UserProfileUpdatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new UpdateUserCommand(
            integrationEvent.UserId,
            integrationEvent.Username,
            integrationEvent.Email,
            integrationEvent.FirstName,
            integrationEvent.LastName,
            integrationEvent.Mobile),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(UpdateUserCommand), result.Error);
        }
    }
}
