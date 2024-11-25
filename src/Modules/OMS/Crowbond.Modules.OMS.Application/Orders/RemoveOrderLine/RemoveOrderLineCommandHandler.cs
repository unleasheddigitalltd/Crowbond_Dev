using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;

namespace Crowbond.Modules.OMS.Application.Orders.RemoveOrderLine;

internal sealed class RemoveOrderLineCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveOrderLineCommand>
{
    public async Task<Result> Handle(RemoveOrderLineCommand request, CancellationToken cancellationToken)
    {
        OrderLine? orderLine = await orderRepository.GetLineAsync(request.OrderLineId, cancellationToken);

        if (orderLine is null)
        {
            return Result.Failure(OrderErrors.LineNotFound(request.OrderLineId));
        }

        Result result = orderLine.Header.RemoveLine(orderLine.Id);

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
