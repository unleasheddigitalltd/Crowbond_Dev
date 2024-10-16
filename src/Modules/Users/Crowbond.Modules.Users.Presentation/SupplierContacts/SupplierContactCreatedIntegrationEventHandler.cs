using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.IntegrationEvents;
using Crowbond.Modules.Users.Application.Users.CreateUser;
using Crowbond.Modules.Users.Domain.Users;
using MediatR;

namespace Crowbond.Modules.Users.Presentation.SupplierContacts;

internal sealed class SupplierContactCreatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<SupplierContactCreatedIntegrationEvent>
{
    public override async Task Handle(
        SupplierContactCreatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateUserCommand(
                integrationEvent.UserId,
                integrationEvent.Email,
                integrationEvent.Username,
                integrationEvent.FirstName,
                integrationEvent.LastName,
                integrationEvent.Mobile,
                Role.Supplier),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateUserCommand), result.Error);
        }
    }
}
