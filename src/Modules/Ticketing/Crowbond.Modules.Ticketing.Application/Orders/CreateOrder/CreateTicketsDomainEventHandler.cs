using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Ticketing.Application.Tickets.CreateTicketBatch;
using Crowbond.Modules.Ticketing.Domain.Orders;
using Crowbond.Common.Application.Exceptions;
using MediatR;

namespace Crowbond.Modules.Ticketing.Application.Orders.CreateOrder;

internal sealed class CreateTicketsDomainEventHandler(ISender sender)
    : DomainEventHandler<OrderCreatedDomainEvent>
{
    public override async Task Handle(
        OrderCreatedDomainEvent notification,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new CreateTicketBatchCommand(notification.OrderId), cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateTicketBatchCommand), result.Error);
        }
    }
}
