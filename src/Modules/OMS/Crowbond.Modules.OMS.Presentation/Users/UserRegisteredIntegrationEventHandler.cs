using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Users.CreateUser;
using Crowbond.Modules.Users.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.OMS.Presentation.Users;

internal sealed class UserRegisteredIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public override async Task Handle(
        UserRegisteredIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new CreateUserCommand(
            integrationEvent.UserId,
            integrationEvent.Username,
            integrationEvent.Email,
            integrationEvent.FirstName,
            integrationEvent.LastName,
            integrationEvent.Mobile),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateUserCommand), result.Error);
        }
    }
}
