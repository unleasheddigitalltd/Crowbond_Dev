namespace Crowbond.Modules.OMS.Domain.Orders;

public interface IOrderRepository
{
    Task<OrderHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(OrderHeader orderheader);
}
