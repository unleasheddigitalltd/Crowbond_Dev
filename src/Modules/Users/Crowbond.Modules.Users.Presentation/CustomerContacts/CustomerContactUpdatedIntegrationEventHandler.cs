using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.IntegrationEvents;
using Crowbond.Modules.Users.Application.Users.UpdateUser;
using MediatR;

namespace Crowbond.Modules.Users.Presentation.CustomerContacts;

internal sealed class CustomerContactUpdatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<CustomerContactUpdatedIntegrationEvent>
{
    public override async Task Handle(
        CustomerContactUpdatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateUserCommand(
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
