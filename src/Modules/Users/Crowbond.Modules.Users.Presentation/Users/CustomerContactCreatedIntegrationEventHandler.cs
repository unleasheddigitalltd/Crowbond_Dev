using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.IntegrationEvents;
using Crowbond.Modules.Users.Application.Users.CreateCustomer;
using MediatR;

namespace Crowbond.Modules.Users.Presentation.Users;

internal sealed class CustomerContactCreatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<CustomerContactCreatedIntegrationEvent>
{    public override async Task Handle(
        CustomerContactCreatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateCustomerCommand(
                integrationEvent.UserId,
                integrationEvent.Email,
                integrationEvent.Username,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateCustomerCommand), result.Error);
        }
    }
}
