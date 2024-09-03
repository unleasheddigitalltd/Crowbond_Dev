using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Products;
using TaskHeader = Crowbond.Modules.WMS.Domain.Tasks.TaskHeader;

namespace Crowbond.Modules.WMS.Domain.Stocks;

public sealed class StockTransaction : Entity
{
    private StockTransaction()
    {
    }

    public Guid Id { get; private set; }

    public Guid? TaskAssignmentLineId { get; private set; }

    public string ActionTypeName { get; private set; }

    public bool PosAdjustment { get; private set; }

    public DateTime TransactionDate { get; private set; }

    public string? TransactionNote { get; private set; }

    public Guid? ReasonId { get; private set; }

    public decimal Quantity { get; private set; }

    public Guid ProductId { get; private set; }

    public Guid StockId { get; private set; }

    internal StockTransaction(
        Guid? taskAssignmentLineId,
        string actionTypeName,
        bool posAdjustment,
        DateTime transactionDate,
        string? transactionNote,
        Guid? reasonId,
        decimal quantity,
        Guid productId)
    {
        Id = Guid.NewGuid();
        TaskAssignmentLineId = taskAssignmentLineId;
        ActionTypeName = actionTypeName;
        PosAdjustment = posAdjustment;
        TransactionDate = transactionDate;
        TransactionNote = transactionNote;
        ReasonId = reasonId;
        Quantity = quantity;
        ProductId = productId;
    }
}
