using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Stocks;

public sealed class StockTransaction : Entity
{
    public StockTransaction()
    {
    }

    public Guid Id { get; private set; }

    public Guid TaskId { get; private set; }

    public string ActionTypeName { get; private set; }

    public bool PosAdjustment { get; private set; }

    public DateTime TransactionDate { get; private set; }

    public string? TransactionNote { get; private set; }

    public decimal Quantity { get; private set; }

    public Guid ProductId { get; private set; }

    public Guid StockId { get; private set; }

    public static StockTransaction Create(
        Guid taskId,
        string actionTypeName,
        bool posAdjustment,
        DateTime transactionDate,
        string? transactionNote,
        decimal quantity,
        Guid productId,
        Guid stockId)
    {
        var stockTransaction = new StockTransaction
        {
            Id = Guid.NewGuid(),
            TaskId = taskId,
            ActionTypeName = actionTypeName,
            PosAdjustment = posAdjustment,
            TransactionDate = transactionDate,
            TransactionNote = transactionNote,
            Quantity = quantity,
            ProductId = productId,
            StockId = stockId
        };

        return stockTransaction;
    }
}
