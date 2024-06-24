namespace Crowbond.Modules.WMS.Domain.Stocks;
public interface IStockRepository
{
    Task<Stock?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<StockTransactionReason?> GetTransactionReasonAsync(Guid id, CancellationToken cancellationToken);

    void InsertTransaction(StockTransaction transaction);
}
