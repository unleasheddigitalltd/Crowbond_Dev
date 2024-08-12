using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;

namespace Crowbond.Modules.OMS.Infrastructure.PurchaseOrderHeaders;

internal sealed class PurchaseOrderRepository(OmsDbContext context) : IPurchaseOrderRepository
{
    public void AddLines(IEnumerable<PurchaseOrderLine> purchaseOrderLines)
    {
        context.PurchaseOrderLines.AddRange(purchaseOrderLines);
    }

    public async Task<PurchaseOrderHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.PurchaseOrderHeaders.Include(p => p.PurchaseOrderLines).SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public void Insert(PurchaseOrderHeader purchaseorderheader)
    {
        context.PurchaseOrderHeaders.Add(purchaseorderheader);
    }

    public void InsertHistory(PurchaseOrderStatusHistory orderStatusHistory)
    {
        context.PurchaseOrderStatusHistories.Add(orderStatusHistory);
    }
}
