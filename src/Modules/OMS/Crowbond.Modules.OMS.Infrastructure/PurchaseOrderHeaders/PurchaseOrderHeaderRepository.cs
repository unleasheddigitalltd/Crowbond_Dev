using Crowbond.Modules.OMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Crowbond.Modules.OMS.Domain.PurchaseOrderHeaders;

namespace Crowbond.Modules.OMS.Infrastructure.PurchaseOrderHeaders;

internal sealed class PurchaseOrderHeaderRepository(OmsDbContext context) : IPurchaseOrderHeaderRepository
{
    public async Task<PurchaseOrderHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.PurchaseOrderHeaders.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Insert(PurchaseOrderHeader purchaseorderheader)
    {
        context.PurchaseOrderHeaders.Add(purchaseorderheader);
    }
}
