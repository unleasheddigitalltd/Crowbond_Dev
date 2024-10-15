using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;

namespace Crowbond.Modules.OMS.Application.Orders.RemoveOrderDeliveryImage;

internal sealed class RemoveOrderDeliveryImageCommandHandler(
    IOrderRepository orderRepository,
    IOrderFileAccess orderFileAccess,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveOrderDeliveryImageCommand>
{
    public async Task<Result> Handle(RemoveOrderDeliveryImageCommand request, CancellationToken cancellationToken)
    {
        OrderHeader? order = await orderRepository.GetWithImagesAsync(request.OrderId, cancellationToken);

        if (order == null)
        {
            return Result.Failure(OrderErrors.NotFound(request.OrderId));
        }

        Result<OrderDeliveryImage> result = order.RemoveDeliveryImage(request.ImageName);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        orderRepository.RemoveDeliveryImage(result.Value);

        await orderFileAccess.DeleteDeliveryImageAsync(request.ImageName, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
