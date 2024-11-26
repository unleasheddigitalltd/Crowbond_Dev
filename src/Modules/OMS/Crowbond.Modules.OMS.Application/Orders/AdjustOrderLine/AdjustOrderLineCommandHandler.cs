using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;

namespace Crowbond.Modules.OMS.Application.Orders.AdjustOrderLine;

internal sealed class AdjustOrderLineCommandHandler(
    IOrderRepository orderRepository,
    InventoryService inventoryService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AdjustOrderLineCommand>
{
    public async Task<Result> Handle(AdjustOrderLineCommand request, CancellationToken cancellationToken)
    {
        OrderLine? orderLine = await orderRepository.GetLineAsync(request.OrderLineId, cancellationToken);

        if (orderLine is null)
        {
            return Result.Failure(OrderErrors.LineNotFound(request.OrderLineId));
        }

        OrderHeader? order = await orderRepository.GetAsync(orderLine.OrderHeaderId, cancellationToken);

        if (order is null)
        {
            return Result.Failure(OrderErrors.NotFound(orderLine.OrderHeaderId));
        }

        decimal availableQty = await inventoryService.GetAvailableQuantityAsync(orderLine.ProductId, cancellationToken);

        if (availableQty >= orderLine.OrderedQty)
        {
            return Result.Failure(OrderErrors.NoShortage);
        }

        Result result = order.AdjustLineOrderedQty(orderLine.Id, availableQty);

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
