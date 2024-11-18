using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;

namespace Crowbond.Modules.OMS.Application.Orders.RemoveOrder;

internal sealed class RemoveOrderCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveOrderCommand>
{
    public async Task<Result> Handle(RemoveOrderCommand request, CancellationToken cancellationToken)
    {
        OrderHeader? orderHeader = await orderRepository.GetAsync(request.OrderId, cancellationToken);

        if (orderHeader is null)
        {
            return Result.Failure(OrderErrors.NotFound(request.OrderId));
        }

        Result validateRemovalResult = orderHeader.ValidateRemoval();
        if (validateRemovalResult.IsFailure)
        {
            return validateRemovalResult;
        }

        orderRepository.Remove(orderHeader);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
