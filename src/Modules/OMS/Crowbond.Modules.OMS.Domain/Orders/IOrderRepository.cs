namespace Crowbond.Modules.OMS.Domain.Orders;

public interface IOrderRepository
{
    void InsertOrderHeader(OrderHeader orderHeader);
}
