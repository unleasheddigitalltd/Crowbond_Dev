using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Crowbond.Modules.OMS.Domain.Orders;

namespace Crowbond.Modules.OMS.Infrastructure.Orders;

internal sealed class OrderRepository(OmsDbContext context) : IOrderRepository
{
    public async Task<OrderHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.OrderHeaders.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Insert(OrderHeader orderheader)
    {
        context.OrderHeaders.Add(orderheader);
    }
}
