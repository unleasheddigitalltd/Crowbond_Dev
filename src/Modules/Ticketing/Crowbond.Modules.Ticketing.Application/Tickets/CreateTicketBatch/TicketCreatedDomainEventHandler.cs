using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Ticketing.Application.Tickets.GetTicket;
using Crowbond.Modules.Ticketing.Domain.Tickets;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Modules.Ticketing.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.Ticketing.Application.Tickets.CreateTicketBatch;

internal sealed class TicketCreatedDomainEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<TicketCreatedDomainEvent>
{
    public override async Task Handle(
        TicketCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<TicketResponse> result = await sender.Send(
            new GetTicketQuery(domainEvent.TicketId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(GetTicketQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new TicketIssuedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.CustomerId,
                result.Value.EventId,
                result.Value.Code),
            cancellationToken);
    }
}
