namespace Crowbond.Modules.OMS.Domain.DeliveryImages;

public interface IDeliveryImageRepository
{
    Task<DeliveryImage?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(DeliveryImage deliveryImage);
}
