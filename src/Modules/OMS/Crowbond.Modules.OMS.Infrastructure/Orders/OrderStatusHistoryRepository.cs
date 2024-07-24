using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Domain.Orders;

internal sealed class OrderStatusHistoryRepository(OmsDbContext context) : IOrderStatusHistoryRepository
{
    public async Task<OrderStatusHistory> GetAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        return await context.OrderStatusHistories.SingleOrDefaultAsync(o => o.Id == Id, cancellationToken);
    }

    public void Insert(OrderStatusHistory orderStatusHistory)
    {
        context.OrderStatusHistories.Add(orderStatusHistory);
    }
}
