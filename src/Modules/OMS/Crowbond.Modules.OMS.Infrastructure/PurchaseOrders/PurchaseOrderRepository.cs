using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;
using Crowbond.Modules.OMS.Domain.Sequences;

namespace Crowbond.Modules.OMS.Infrastructure.PurchaseOrders;

internal sealed class PurchaseOrderRepository(OmsDbContext context) : IPurchaseOrderRepository
{
    public void AddLine(PurchaseOrderLine purchaseOrderLine)
    {
        context.PurchaseOrderLines.Add(purchaseOrderLine);
    }

    public void AddLines(IEnumerable<PurchaseOrderLine> purchaseOrderLines)
    {
        context.PurchaseOrderLines.AddRange(purchaseOrderLines);
    }

    public async Task<PurchaseOrderHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.PurchaseOrderHeaders.Include(p => p.Lines).Include(p => p.StatusHistory).SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<PurchaseOrderLine?> GetLineAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.PurchaseOrderLines.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<PurchaseOrderHeader?> GetDraftBySupplierIdAsync(Guid supplierId, CancellationToken cancellationToken = default)
    {
        return await context.PurchaseOrderHeaders.Include(p => p.Lines).Include(p => p.StatusHistory).SingleOrDefaultAsync(p => p.SupplierId == supplierId && p.Status == PurchaseOrderStatus.Draft, cancellationToken);
    }

    public async Task<Sequence?> GetSequenceAsync(CancellationToken cancellationToken = default)
    {
        return await context.Sequences.SingleOrDefaultAsync(s => s.Context == SequenceContext.PurchaseOrder, cancellationToken);
    }

    public void Insert(PurchaseOrderHeader purchaseorderheader)
    {
        context.PurchaseOrderHeaders.Add(purchaseorderheader);
    }

    public void InsertHistory(PurchaseOrderStatusHistory orderStatusHistory)
    {
        context.PurchaseOrderStatusHistories.Add(orderStatusHistory);
    }

    public async Task<List<PurchaseOrderLine>> GetLinesPendingByProductAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return await (from line in context.PurchaseOrderLines
                      join order in context.PurchaseOrderHeaders on line.PurchaseOrderHeaderId equals order.Id
                      where line.ProductId == productId && (order.Status == PurchaseOrderStatus.Draft || order.Status == PurchaseOrderStatus.Pending)
                      select line)
                 .ToListAsync(cancellationToken);
    }
}
