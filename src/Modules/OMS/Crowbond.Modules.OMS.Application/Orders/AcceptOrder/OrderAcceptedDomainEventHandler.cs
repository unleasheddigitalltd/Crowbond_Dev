using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Orders.GetOrderWithLines;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.OMS.Application.Orders.AcceptOrder;

internal sealed class OrderAcceptedDomainEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<OrderAcceptedDomainEvent>
{
    public override async Task Handle(
        OrderAcceptedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<OrderResponse> result = await sender.Send(
            new GetOrderWithLinesQuery(domainEvent.OrderId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(GetOrderWithLinesQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new OrderAcceptedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.OrderNo,
                result.Value.Lines.Select(l => new OrderAcceptedIntegrationEvent.OrderLine(
                    l.ProductId, l.OrderedQty)).ToList()),
            cancellationToken);
    }
}
