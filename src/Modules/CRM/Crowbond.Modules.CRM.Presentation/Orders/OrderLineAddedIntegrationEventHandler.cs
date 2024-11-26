using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Exceptions;
using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.CustomerProducts.CreateCustomerProductByOrder;
using Crowbond.Modules.OMS.IntegrationEvents;
using MediatR;

namespace Crowbond.Modules.CRM.Presentation.Orders;

internal sealed class OrderLineAddedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<OrderLineAddedIntegrationEvent>
{
    public override async Task Handle(
        OrderLineAddedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateCustomerProductByOrderCommand(
                integrationEvent.CustomerId,
                integrationEvent.ProductId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new CrowbondException(nameof(CreateCustomerProductByOrderCommand), result.Error);
        }
    }
}
