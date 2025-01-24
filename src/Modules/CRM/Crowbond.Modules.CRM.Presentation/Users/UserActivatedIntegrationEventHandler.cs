using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.Users.ActivateUser;
using Crowbond.Modules.Users.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.CRM.Presentation.Users;

internal sealed class UserActivatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserActivatedIntegrationEvent>
{
    public override async Task Handle(
        UserActivatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new ActivateUserCommand(
            integrationEvent.UserId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(ActivateUserCommand), result.Error);
        }
    }
}
