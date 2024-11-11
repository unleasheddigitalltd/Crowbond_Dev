using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Orders;

public sealed class OrderDeliveryImage : Entity
{
    private OrderDeliveryImage()
    {
    }

    public Guid Id { get; private set; }

    public Guid OrderDeliveryId { get; private set; }

    public string ImageName { get; private set; }

    internal static OrderDeliveryImage Create(string imageName)
    {
        var deliveryImage = new OrderDeliveryImage
        {
            Id = Guid.NewGuid(),
            ImageName = imageName
        };

        return deliveryImage;
    }
}
