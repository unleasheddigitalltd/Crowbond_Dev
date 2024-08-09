using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Drivers.GetDriver;
using Crowbond.Modules.OMS.Domain.Drivers;
using Crowbond.Modules.OMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.OMS.Application.Drivers.CreateDriver;

internal sealed class DriverCreatedIntegrationDomainEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<DriverCreatedDomainEvent>
{
    public override async Task Handle(
        DriverCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<DriverResponse> result = await sender.Send(
            new GetDriverQuery(domainEvent.DriverId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(GetDriverQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new DriverCreatedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Username,
                result.Value.Email,
                result.Value.FirstName,
                result.Value.LastName),
            cancellationToken);
    }
}
