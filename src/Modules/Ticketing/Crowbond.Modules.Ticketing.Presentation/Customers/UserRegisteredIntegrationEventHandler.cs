using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Domain;
using Crowbond.Modules.Ticketing.Application.Customers.CreateCustomer;
using Crowbond.Modules.Users.IntegrationEvents;
using Crowbond.Common.Application.Exceptions;
using MediatR;

namespace Crowbond.Modules.Ticketing.Presentation.Customers;

internal sealed class UserRegisteredIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public override async Task Handle(
        UserRegisteredIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateCustomerCommand(
                integrationEvent.UserId,
                integrationEvent.Email,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateCustomerCommand), result.Error);
        }
    }
}
