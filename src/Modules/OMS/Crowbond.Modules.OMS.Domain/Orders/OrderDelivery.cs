using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Orders;

public sealed class OrderDelivery : Entity
{
    private readonly List<OrderDeliveryImage> _images = new();

    private OrderDelivery()
    {
    }

    public Guid Id { get; private set; }

    public Guid RouteTripLogId { get; private set; }

    public Guid OrderHeaderId { get; private set; }

    public DeliveryStatus Status { get; private set; }

    public DateTime DateTime { get; private set; }

    public string? Comments { get; private set; }

    public IReadOnlyCollection<OrderDeliveryImage> Images => _images;

    internal static OrderDelivery Create(
        Guid routeTripLogId,
        DeliveryStatus status,
        DateTime dateTime,
        string? Comments)
    {
        var delivery = new OrderDelivery
        {
            Id = Guid.NewGuid(),
            RouteTripLogId = routeTripLogId,
            Status = status,
            DateTime = dateTime,
            Comments = Comments
        };

        return delivery;
    }

    internal Result<OrderDeliveryImage> AddImage(string imageName)
    {
        var image = OrderDeliveryImage.Create(imageName);
        _images.Add(image);

        return Result.Success(image);
    }

    internal Result<OrderDeliveryImage> RemoveImage(string imageName)
    {
        OrderDeliveryImage? image = Images.SingleOrDefault(i => i.ImageName == imageName);

        if (image is null)
        {
            return Result.Failure<OrderDeliveryImage>(OrderErrors.DeliveryImageNotFound(imageName));
        }

        _images.Remove(image);

        return Result.Success(image);
    }
}
