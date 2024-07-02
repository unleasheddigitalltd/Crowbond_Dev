using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Products;
using Task = Crowbond.Modules.WMS.Domain.Tasks.Task;

namespace Crowbond.Modules.WMS.Domain.Stocks;

public sealed class StockTransaction : Entity
{
    public StockTransaction()
    {
    }

    public Guid Id { get; private set; }

    public Guid? TaskId { get; private set; }

    public string ActionTypeName { get; private set; }

    public bool PosAdjustment { get; private set; }

    public DateTime TransactionDate { get; private set; }

    public string? TransactionNote { get; private set; }

    public Guid? ReasonId { get; private set; }

    public decimal Quantity { get; private set; }

    public Guid ProductId { get; private set; }

    public Guid StockId { get; private set; }

    public static StockTransaction Create(
        Task? task,
        string actionTypeName,
        bool posAdjustment,
        DateTime transactionDate,
        string? transactionNote,
        StockTransactionReason? reason,
        decimal quantity,
        Product product,
        Stock stock)
    {
        var stockTransaction = new StockTransaction
        {
            Id = Guid.NewGuid(),
            TaskId = task?.Id,
            ActionTypeName = actionTypeName,
            PosAdjustment = posAdjustment,
            TransactionDate = transactionDate,
            TransactionNote = transactionNote,
            ReasonId = reason?.Id,
            Quantity = quantity,
            ProductId = product.Id,
            StockId = stock.Id
        };

        return stockTransaction;
    }
}
