using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Stocks;

public sealed class Stock : Entity , IAuditable
{
    private readonly List<StockTransaction> _stockTransactions = new();
    private Stock()
    {
    }

    public Guid Id { get; private set; }

    public Guid ProductId { get; private set; }

    public Guid LocationId { get; private set; }

    public decimal OriginalQty { get; private set; }

    public decimal CurrentQty { get; private set; }

    public string BatchNumber { get; private set; }

    public DateOnly ReceivedDate { get; private set; }

    public DateOnly? SellByDate { get; private set; }

    public DateOnly? UseByDate { get; private set; }

    public Guid ReceiptLineId { get; private set; }

    public string? Note { get; private set; }

    public StockStatus Status { get; private set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOnUtc { get; set; }

    public IReadOnlyCollection<StockTransaction> StockTransactions => _stockTransactions;

    public static Result<Stock> Create(
        Guid productId,
        Guid locationId,
        string batchNumber,
        DateOnly receivedDate,
        DateOnly? sellByDate,
        DateOnly? useByDate,
        Guid receiptLineId,
        string? note)
    {
        var stock = new Stock
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            LocationId = locationId,
            OriginalQty = 0,
            CurrentQty = 0,
            BatchNumber = batchNumber,
            ReceivedDate = receivedDate,
            SellByDate = sellByDate,
            UseByDate = useByDate,
            ReceiptLineId = receiptLineId,
            Note = note,
            Status = StockStatus.Active
        };

        return stock;
    }

    public Result<StockTransaction> StockIn(
        Guid? taskAssignmentLineId,
        string actionTypeName,
        DateTime transactionDate,
        string? transactionNote,
        Guid? reasonId,
        decimal quantity)
    {
        if (quantity <= 0)
        {
            return Result.Failure<StockTransaction>(StockErrors.NonPositiveQty);
        }

        var transaction = new StockTransaction(
            taskAssignmentLineId,
            actionTypeName,
            true,
            transactionDate,
            transactionNote,
            reasonId,
            quantity,
            ProductId);

        _stockTransactions.Add(transaction);
        ApplyTransaction(quantity, true);

        return Result.Success(transaction);
    }

    public Result<StockTransaction> StockOut(
        Guid? taskAssignmentLineId,
        string actionTypeName,
        DateTime transactionDate,
        string? transactionNote,
        Guid? reasonId,
        decimal quantity)
    {
        if (quantity <= 0)
        {
            return Result.Failure<StockTransaction>(StockErrors.NonPositiveQty);
        }

        if (quantity > CurrentQty)
        {
            return Result.Failure<StockTransaction>(StockErrors.OverdrawnStock);
        }

        var transaction = new StockTransaction(
            taskAssignmentLineId,
            actionTypeName,
            false,
            transactionDate,
            transactionNote,
            reasonId,
            quantity,
            ProductId);

        _stockTransactions.Add(transaction);
        ApplyTransaction(quantity, false);

        return Result.Success(transaction);
    }


    private void ApplyTransaction(decimal quantity, bool posAdjustment)
    {
        CurrentQty = posAdjustment ? CurrentQty + quantity : CurrentQty - quantity;
    }

    public Result Hold()
    {
        if (Status != StockStatus.Active)
        {
            return Result.Failure(StockErrors.NotActive);
        }

        Status = StockStatus.Held;

        return Result.Success();
    }

    public Result Activate()
    {
        if (Status != StockStatus.Held)
        {
            return Result.Failure(StockErrors.NotHeld);
        }

        Status = StockStatus.Active;

        return Result.Success();
    }
}
