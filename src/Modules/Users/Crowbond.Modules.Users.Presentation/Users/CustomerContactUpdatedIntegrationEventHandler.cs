using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.IntegrationEvents;
using Crowbond.Modules.Users.Application.Users.UpdateUser;
using MediatR;

namespace Crowbond.Modules.Users.Presentation.Users;

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
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(UpdateUserCommand), result.Error);
        }
    }
}
