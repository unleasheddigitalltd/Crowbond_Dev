namespace Crowbond.Modules.OMS.Domain.Deliveries;

public interface IDeliveryRepository
{
    Task<Delivery?> GetAsync(Guid Id, CancellationToken cancellationToken = default);

    void Insert(Delivery delivery);
}
