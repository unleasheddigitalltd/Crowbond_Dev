using System.Drawing;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.OMS.Application.Abstractions.Data;
using Crowbond.Modules.OMS.Domain.Orders;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace Crowbond.Modules.OMS.Application.Orders.UploadOrderDeliveryImages;

internal sealed class UploadOrderDeliveryImagesCommandHandler(
    IOrderRepository orderRepository,
    IOrderFileAccess orderFileAccess,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UploadOrderDeliveryImagesCommand>
{
    public async Task<Result> Handle(UploadOrderDeliveryImagesCommand request, CancellationToken cancellationToken)
    {

        if (request.Images == null || request.Images.Count == 0)
        {
            return Result.Failure(OrderErrors.NoFilesUploaded);
        }

        var _allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif" };
        foreach (IFormFile image in request.Images)
        {
            if (image.Length > 1024 * 1024) // 1MB
            {
                return Result.Failure(OrderErrors.FileSizeExceeds);
            }

            string fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(fileExtension))
            {
                return Result.Failure(OrderErrors.InvalidFileType(_allowedExtensions));
            }
        }

        OrderHeader? order = await orderRepository.GetAsync(request.OrderHeaderId, cancellationToken);

        if (order == null)
        {
            return Result.Failure(OrderErrors.NotFound(request.OrderHeaderId));
        }

        Result<OrderDelivery> deliveryResult = order.CheckIsDeliver();

        if (deliveryResult.IsFailure)
        {
            return deliveryResult;
        }

        List<string> imagesUrl = await orderFileAccess.SaveDeliveryImagesAsync(order.OrderNo, order.LastImageSequence, request.Images, cancellationToken);

        foreach (string imageUrl in imagesUrl)
        {
            OrderDeliveryImage deliveryImage = order.AddDeliveryImage(deliveryResult.Value, imageUrl);
            orderRepository.AddDeliveryImage(deliveryImage);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
