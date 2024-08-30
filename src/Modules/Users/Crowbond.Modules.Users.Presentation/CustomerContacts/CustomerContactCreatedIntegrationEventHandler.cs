using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.IntegrationEvents;
using Crowbond.Modules.Users.Application.Users.CreateUser;
using Crowbond.Modules.Users.Domain.Users;
using MediatR;

namespace Crowbond.Modules.Users.Presentation.CustomerContacts;

internal sealed class CustomerContactCreatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<CustomerContactCreatedIntegrationEvent>
{
    public override async Task Handle(
        CustomerContactCreatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateUserCommand(
                integrationEvent.UserId,
                integrationEvent.Email,
                integrationEvent.Username,
                integrationEvent.FirstName,
                integrationEvent.LastName,
                Role.Customer),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateUserCommand), result.Error);
        }
    }
}
