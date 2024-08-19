using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.IntegrationEvents;
using Crowbond.Modules.Users.Application.Users.CreateUser;
using Crowbond.Modules.Users.Application.Users.DeactivateUser;
using MediatR;

namespace Crowbond.Modules.Users.Presentation.CustomerContacts;

internal sealed class CustomerContactDeactivatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<CustomerContactDeactivatedIntegrationEvent>
{
    public override async Task Handle(
        CustomerContactDeactivatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new DeactivateUserCommand(integrationEvent.UserId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateUserCommand), result.Error);
        }
    }
}
