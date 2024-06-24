using Crowbond.Modules.WMS.Domain.Stocks;
using Crowbond.Modules.WMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.WMS.Infrastructure.Stocks;

internal sealed class StockRepository(WmsDbContext context) : IStockRepository
{
    public async Task<Stock?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Stocks.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<StockTransactionReason?> GetTransactionReasonAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.StockTransactionReasons.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void InsertTransaction(StockTransaction transaction)
    {
        context.StockTransactions.Add(transaction);
    }
}
