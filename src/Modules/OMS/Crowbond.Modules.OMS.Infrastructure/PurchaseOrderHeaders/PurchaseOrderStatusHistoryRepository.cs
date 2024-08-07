using Crowbond.Modules.OMS.Domain.PurchaseOrderHeaders;
using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.OMS.Infrastructure.PurchaseOrderHeaders;

internal sealed class PurchaseOrderStatusHistoryRepository(OmsDbContext context) : IPurchaseOrderStatusHistoryRepository
{
    public async Task<PurchaseOrderStatusHistory?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {        
        return await context.PurchaseOrderStatusHistories.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public void Insert(PurchaseOrderStatusHistory orderStatusHistory)
    {
        context.PurchaseOrderStatusHistories.Add(orderStatusHistory);
    }
}
