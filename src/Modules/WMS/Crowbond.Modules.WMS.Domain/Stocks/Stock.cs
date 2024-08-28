using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Stocks;

public sealed class Stock : Entity
{
    public Stock()
    {
    }

    public Guid Id { get; private set; }

    public Guid ProductId { get; private set; }

    public Guid LocationId { get; private set; }

    public decimal OriginalQty { get; private set; }

    public decimal CurrentQty { get; private set; }

    public string BatchNumber { get; private set; }

    public DateTime ReceivedDate { get; private set; }

    public DateTime? SellByDate { get; private set; }

    public DateTime? UseByDate { get; private set; }

    public Guid ReceiptLineId { get; private set; }

    public string? Note { get; private set; }

    public StockStatus Status { get; private set; }

    public Guid CreatedBy { get; private set; }

    public DateTime CreatedDate { get; private set; }

    public Guid? LastModifiedBy { get; private set; }

    public DateTime? LastModifiedDate { get; private set; }

    public static Stock Create(
        Guid productId,
        Guid locationId,
        decimal originalQty,
        decimal currentQty,
        string batchNumber,
        DateTime receivedDate,
        DateTime? sellByDate,
        DateTime? useByDate,
        Guid receiptLineId,
        string? note, 
        Guid createdBy,
        DateTime createdDate)
    {
        var stock = new Stock
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            LocationId = locationId,
            OriginalQty = originalQty,
            CurrentQty = currentQty,
            BatchNumber = batchNumber,
            ReceivedDate = receivedDate,
            SellByDate = sellByDate,
            UseByDate = useByDate,
            ReceiptLineId = receiptLineId,
            Note = note,
            Status = StockStatus.Active,
            CreatedBy = createdBy,
            CreatedDate = createdDate
        };

        return stock;
    }

    public void Adjust(Guid lastModifiedBy, DateTime lastModifiedDate, bool posAdjustment, decimal quantity)
    {        
        CurrentQty = posAdjustment ? CurrentQty + quantity : CurrentQty - quantity;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;
    }

    public Result Hold(Guid lastModifiedBy, DateTime lastModifiedDate)
    {
        if (Status != StockStatus.Active)
        {
            return Result.Failure(StockErrors.NotActive);
        }

        Status = StockStatus.Held;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;

        return Result.Success();
    }

    public Result Activate(Guid lastModifiedBy, DateTime lastModifiedDate)
    {
        if (Status != StockStatus.Held)
        {
            return Result.Failure(StockErrors.NotHeld);
        }

        Status = StockStatus.Active;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;

        return Result.Success();
    }

    public Result MarkAsDamaged(Guid lastModifiedBy, DateTime lastModifiedDate)
    {
        Status = StockStatus.Damaged;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;

        return Result.Success();
    }
}
