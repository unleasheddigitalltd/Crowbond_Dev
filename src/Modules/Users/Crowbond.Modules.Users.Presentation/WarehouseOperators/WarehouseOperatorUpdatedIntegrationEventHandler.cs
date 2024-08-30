using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Application.Users.UpdateUser;
using Crowbond.Modules.WMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.Users.Presentation.WarehouseOperators;

internal sealed class WarehouseOperatorUpdatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<WarehouseOperatorCreatedIntegrationEvent>
{
    public override async Task Handle(
        WarehouseOperatorCreatedIntegrationEvent integrationEvent,
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
