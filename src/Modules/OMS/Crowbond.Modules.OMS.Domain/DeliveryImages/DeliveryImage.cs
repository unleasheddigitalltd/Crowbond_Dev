using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.DeliveryImages;

public sealed class DeliveryImage : Entity
{
    public DeliveryImage()
    {        
    }

    public Guid Id { get; private set; }

    public Guid DeliveryId { get; private set; }

    public string ImageId { get; private set; }

    public static DeliveryImage Create(Guid deliveryId, string imageId)
    {
        var deliveryImage = new DeliveryImage
        {
            Id = Guid.NewGuid(),
            DeliveryId = deliveryId,
            ImageId = imageId
        };

        return deliveryImage;
    }
}
