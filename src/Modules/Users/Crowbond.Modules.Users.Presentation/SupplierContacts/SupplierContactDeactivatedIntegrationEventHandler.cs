using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.IntegrationEvents;
using Crowbond.Modules.Users.Application.Users.CreateUser;
using Crowbond.Modules.Users.Application.Users.DeactivateUser;
using MediatR;

namespace Crowbond.Modules.Users.Presentation.SupplierContacts;

internal sealed class SupplierContactDeactivatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<SupplierContactDeactivatedIntegrationEvent>
{
    public override async Task Handle(
        SupplierContactDeactivatedIntegrationEvent integrationEvent,
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
