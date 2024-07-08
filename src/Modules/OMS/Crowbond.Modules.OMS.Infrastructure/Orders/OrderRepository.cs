using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Infrastructure.Database;

namespace Crowbond.Modules.OMS.Infrastructure.Orders;
internal sealed class OrderRepository(OmsDbContext context) : IOrderRepository
{
    public void InsertOrderHeader(OrderHeader orderHeader)
    {
        context.orderHeaders.Add(orderHeader);
    }
}
