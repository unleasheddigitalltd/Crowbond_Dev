using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Events.Application.Events.GetEvent;
using Crowbond.Modules.Events.Domain.Events;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Modules.Events.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.Events.Application.Events.PublishEvent;

internal sealed class EventPublishedDomainEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<EventPublishedDomainEvent>
{
    public override async Task Handle(
        EventPublishedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<EventResponse> result = await sender.Send(new GetEventQuery(domainEvent.EventId), cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(GetEventQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new EventPublishedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Title,
                result.Value.Description,
                result.Value.Location,
                result.Value.StartsAtUtc,
                result.Value.EndsAtUtc,
                result.Value.TicketTypes.Select(t => new TicketTypeModel
                {
                    Id = t.TicketTypeId,
                    EventId = result.Value.Id,
                    Name = t.Name,
                    Price = t.Price,
                    Currency = t.Currency,
                    Quantity = t.Quantity
                }).ToList()),
            cancellationToken);
    }
}
