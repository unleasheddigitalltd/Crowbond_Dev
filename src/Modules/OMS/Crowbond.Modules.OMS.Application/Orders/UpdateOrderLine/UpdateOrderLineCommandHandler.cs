using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;

namespace Crowbond.Modules.OMS.Application.Orders.UpdateOrderLine;

internal sealed class UpdateOrderLineCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateOrderLineCommand>
{
    public async Task<Result> Handle(UpdateOrderLineCommand request, CancellationToken cancellationToken)
    {
        OrderLine? orderLine = await orderRepository.GetLineAsync(request.OrderLineId, cancellationToken);

        if (orderLine == null)
        {
            return Result.Failure(OrderErrors.LineNotFound(request.OrderLineId));
        }

        Result result = orderLine.Header.AdjustLineOrderedQty(orderLine.Id, request.Qty);

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
