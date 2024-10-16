using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Users.CreateUser;
using Crowbond.Modules.Users.Domain.Users;
using Crowbond.Modules.WMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.Users.Presentation.WarehouseOperators;

internal sealed class WarehouseOperatorCreatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<WarehouseOperatorCreatedIntegrationEvent>
{
    public override async Task Handle(
        WarehouseOperatorCreatedIntegrationEvent integrationEvent,
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
                Role.WarehouseOperator),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateUserCommand), result.Error);
        }
    }
}
