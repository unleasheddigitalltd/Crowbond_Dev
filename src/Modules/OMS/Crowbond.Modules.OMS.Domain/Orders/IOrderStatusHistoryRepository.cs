namespace Crowbond.Modules.OMS.Domain.Orders;

public interface IOrderStatusHistoryRepository
{
    Task<OrderStatusHistory?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(OrderStatusHistory orderStatusHistory);
}
