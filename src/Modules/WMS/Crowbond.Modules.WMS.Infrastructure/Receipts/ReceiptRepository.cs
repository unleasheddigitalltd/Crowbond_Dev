using Crowbond.Modules.WMS.Domain.Receipts;
using Crowbond.Modules.WMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.WMS.Infrastructure.Receipts;

internal sealed class ReceiptRepository(WmsDbContext context) : IReceiptRepository
{
    public async Task<ReceiptLine?> GetReceiptLineAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.ReceiptLines.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void InsertRangeReceiptLines(IEnumerable<ReceiptLine> receiptLines)
    {
        context.ReceiptLines.AddRange(receiptLines);
    }

    public void InsertReceiptHeader(ReceiptHeader receiptHeader)
    {
        context.ReceiptHeaders.Add(receiptHeader);
    }

    public void InsertReceiptLine(ReceiptLine receiptLine)
    {
        context.ReceiptLines.Add(receiptLine);
    }
}
