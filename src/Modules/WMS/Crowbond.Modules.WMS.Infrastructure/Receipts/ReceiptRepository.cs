using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Domain.Sequences;
using Crowbond.Modules.WMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.WMS.Infrastructure.Receipts;

internal sealed class ReceiptRepository(WmsDbContext context) : IReceiptRepository
{
    public void AddLines(IEnumerable<ReceiptLine> purchaseOrderLines)
    {
        context.ReceiptLines.AddRange(purchaseOrderLines);
    }

    public async Task<ReceiptHeader?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.ReceiptHeaders.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ReceiptLine>> GetLinesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.ReceiptLines.Where(r => r.ReceiptHeaderId == id).ToListAsync(cancellationToken);
    }

    public async Task<ReceiptHeader?> GetByPurchaseOrderIdAsync(Guid purchaseOrderId, CancellationToken cancellationToken = default)
    {
        return await context.ReceiptHeaders.SingleOrDefaultAsync(r => r.PurchaseOrderId == purchaseOrderId, cancellationToken);
    }

    public async Task<Sequence?> GetSequenceAsync(CancellationToken cancellationToken = default)
    {
        return await context.Sequences.SingleOrDefaultAsync(s => s.Context == SequenceContext.Receipt, cancellationToken);
    }

    public void Insert(ReceiptHeader purchaseorderheader)
    {
        context.ReceiptHeaders.Add(purchaseorderheader);
    }

}
