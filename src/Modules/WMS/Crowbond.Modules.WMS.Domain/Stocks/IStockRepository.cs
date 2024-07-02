namespace Crowbond.Modules.WMS.Domain.Stocks;
public interface IStockRepository
{
    Task<Stock?> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<StockTransactionReason?> GetTransactionReasonAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<Stock>> GetByLocationAsync(Guid locationId, CancellationToken cancellationToken);

    void InsertStockTransaction(StockTransaction transaction);

    void InsertStock(Stock stock);
}
