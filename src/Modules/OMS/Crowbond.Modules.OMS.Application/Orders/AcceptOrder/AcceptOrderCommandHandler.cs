using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;

namespace Crowbond.Modules.OMS.Application.Orders.AcceptOrder;

internal sealed class AcceptOrderCommandHandler(
    IOrderRepository orderRepository,
    InventoryService inventoryService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AcceptOrderCommand>
{
    public async Task<Result> Handle(AcceptOrderCommand request, CancellationToken cancellationToken)
    {
        OrderHeader? orderHeader = await orderRepository.GetAsync(request.OrderId, cancellationToken);

        if (orderHeader is null)
        {
            return Result.Failure(OrderErrors.NotFound(request.OrderId));
        }

        foreach (OrderLine line in orderHeader.Lines)
        {
            decimal availableQty = await inventoryService.GetAvailableQuantityAsync(line.ProductId, cancellationToken);

            if (availableQty < line.Qty)
            {
                return Result.Failure(OrderErrors.LineHasShortage(line.Id));
            }
        }

        Result result = orderHeader.Accept();

        if (result.IsFailure)
        {
            return result;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
