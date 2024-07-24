namespace Crowbond.Modules.OMS.Domain.Orders;

public interface IOrderStatusHistoryRepository
{
    Task<OrderStatusHistory> GetAsync(Guid Id, CancellationToken cancellationToken = default);

    void Insert(OrderStatusHistory orderStatusHistory);
}
