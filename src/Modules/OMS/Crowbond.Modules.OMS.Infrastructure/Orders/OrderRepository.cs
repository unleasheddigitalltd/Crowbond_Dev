using Crowbond.Modules.OMS.Domain.Sequences;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.Orders;
internal sealed class OrderRepository(OmsDbContext context) : IOrderRepository
{
    public async Task<OrderHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.OrderHeaders.Include(o => o.Lines).SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<Sequence?> GetSequenceAsync(CancellationToken cancellationToken = default)
    {
        return await context.Sequences.SingleOrDefaultAsync(s => s.Context == SequenceContext.Order, cancellationToken);
    }

    public void Insert(OrderHeader orderHeader)
    {
        context.OrderHeaders.Add(orderHeader);
    }

    public void AddLine(OrderLine line)
    {
        context.OrderLines.Add(line);
    }
}
