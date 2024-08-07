using Crowbond.Modules.OMS.Domain.PurchaseOrderLines;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.PurchaseOrderLines;

internal sealed class PurchaseOrderLineRepository(OmsDbContext context) : IPurchaseOrderLineRepository
{
    public async Task<PurchaseOrderLine?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.PurchaseOrderLines.SingleOrDefaultAsync(pl => pl.Id == id, cancellationToken);
    }

    public void InserRange(IEnumerable<PurchaseOrderLine> purchaseOrderLines)
    {
        context.PurchaseOrderLines.AddRange(purchaseOrderLines);
    }

    public void Insert(PurchaseOrderLine purchaseOrderLine)
    {
        context.PurchaseOrderLines.Add(purchaseOrderLine);
    }
}
