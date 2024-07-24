using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.IntegrationEvents;
using Crowbond.Modules.Users.Application.Users.CreateDriver;
using MediatR;

namespace Crowbond.Modules.Users.Presentation.Users;

internal sealed class DriverCreatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<DriverCreatedIntegrationEvent>
{
    public override async Task Handle(
        DriverCreatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateDriverCommand(
                integrationEvent.UserId,
                integrationEvent.Email,
                integrationEvent.Username,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateDriverCommand), result.Error);
        }
    }
}
