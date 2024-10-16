using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.WarehouseOperators.GerWarehouseOperator;
using Crowbond.Modules.WMS.Domain.WarehouseOperators;
using Crowbond.Modules.WMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.WMS.Application.WarehouseOperators.CreateWarehouseOperator;

internal sealed class WarehouseOperatorCreatedIntegrationDomainEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<WarehouseOperatorCreatedDomainEvent>
{
    public override async Task Handle(
        WarehouseOperatorCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<WarehouseOperatorResponse> result = await sender.Send(
            new GetWarehouseOperatorQuery(domainEvent.OperatorId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(GetWarehouseOperatorQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new WarehouseOperatorCreatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Username,
                result.Value.Email,
                result.Value.FirstName,
                result.Value.LastName,
                result.Value.Mobile),
            cancellationToken);
    }
}
