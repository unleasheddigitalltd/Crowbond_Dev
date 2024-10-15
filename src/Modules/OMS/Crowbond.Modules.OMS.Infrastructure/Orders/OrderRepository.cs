using Crowbond.Modules.OMS.Domain.Sequences;
using Crowbond.Modules.OMS.Domain.Orders;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.Orders;
internal sealed class OrderRepository(OmsDbContext context) : IOrderRepository
{
    public async Task<OrderHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.OrderHeaders.Include(o => o.Lines).Include(o => o.Delivery).SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<OrderHeader?> GetWithImagesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.OrderHeaders.Include(o => o.Lines).Include(o => o.Delivery).ThenInclude(d => d.Images).SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
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

    public async Task<OrderLine?> GetLineAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.OrderLines.Include(ol => ol.Header).SingleOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public void RemoveLine(OrderLine line)
    {
        context.OrderLines.Remove(line);
    }

    public void AddDelivery(OrderDelivery delivery)
    {
        context.OrderDeliveries.Add(delivery);
    }

    public void AddDeliveryImage(OrderDeliveryImage image)
    {
        context.OrderDeliveryImages.Add(image);
    }

    public void RemoveDeliveryImage(OrderDeliveryImage image)
    {
        context.OrderDeliveryImages.Remove(image);
    }
}
