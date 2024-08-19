using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.IntegrationEvents;
using Crowbond.Modules.Users.Application.Users.ActivateUser;
using Crowbond.Modules.Users.Application.Users.CreateUser;
using MediatR;

namespace Crowbond.Modules.Users.Presentation.CustomerContacts;

internal sealed class CustomerContactActivatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<CustomerContactActivatedIntegrationEvent>
{
    public override async Task Handle(
        CustomerContactActivatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new ActivateUserCommand(integrationEvent.UserId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateUserCommand), result.Error);
        }
    }
}
