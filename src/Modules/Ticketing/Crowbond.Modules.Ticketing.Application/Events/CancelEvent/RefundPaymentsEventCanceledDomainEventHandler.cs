using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Ticketing.Application.Payments.RefundPaymentsForEvent;
using Crowbond.Modules.Ticketing.Domain.Events;
using Crowbond.Common.Application.Exceptions;
using MediatR;

namespace Crowbond.Modules.Ticketing.Application.Events.CancelEvent;

internal sealed class RefundPaymentsEventCanceledDomainEventHandler(ISender sender)
    : DomainEventHandler<EventCanceledDomainEvent>
{
    public override async Task Handle(
        EventCanceledDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new RefundPaymentsForEventCommand(domainEvent.EventId), cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(RefundPaymentsForEventCommand), result.Error);
        }
    }
}
