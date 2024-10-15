using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;

namespace Crowbond.Modules.OMS.Application.Orders.UploadOrderDeliveryImage;

internal sealed class UploadOrderDeliveryImageCommandHandler(
    IOrderRepository orderRepository,
    IOrderFileAccess orderFileAccess,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UploadOrderDeliveryImageCommand>
{
    public async Task<Result> Handle(UploadOrderDeliveryImageCommand request, CancellationToken cancellationToken)
    {
        OrderHeader? order = await orderRepository.GetAsync(request.OrderHeaderId, cancellationToken);

        if (order == null)
        {
            return Result.Failure(OrderErrors.NotFound(request.OrderHeaderId));
        }

        string imageUrl = await orderFileAccess.SaveDeliveryImageAsync(order.OrderNo, order.LastImageSequence, request.Image, cancellationToken);

        Result<OrderDeliveryImage> imagesResult = order.AddDeliveryImage(imageUrl);

        if (imagesResult.IsFailure)
        {
            return Result.Failure(imagesResult.Error);
        }

        orderRepository.AddDeliveryImage(imagesResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

