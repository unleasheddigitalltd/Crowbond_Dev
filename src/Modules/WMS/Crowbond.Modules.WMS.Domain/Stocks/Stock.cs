using System.Runtime.InteropServices;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Locations;
using Crowbond.Modules.WMS.Domain.Products;
using Crowbond.Modules.WMS.Domain.Receipts;

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

    public Guid ReceiptId { get; private set; }

    public string? Note { get; private set; }

    public StockStatus Status { get; private set; }

    public static Stock Create(
        Product product,
        Location location,
        decimal originalQty,
        decimal currentQty,
        string batchNumber,
        DateTime receivedDate,
        DateTime? sellByDate,
        DateTime? useByDate,
        ReceiptLine receipt,
        string? note)
    {
        var stock = new Stock
        {
            Id = Guid.NewGuid(),
            ProductId = product.Id,
            LocationId = location.Id,
            OriginalQty = originalQty,
            CurrentQty = currentQty,
            BatchNumber = batchNumber,
            ReceivedDate = receivedDate,
            SellByDate = sellByDate,
            UseByDate = useByDate,
            ReceiptId = receipt.Id,
            Note = note,
            Status = StockStatus.Active
        };

        return stock;
    }

    public void Adjust(bool posAdjustment, decimal quantity)
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

    public Result MarkAsDamaged()
    {
        Status = StockStatus.Damaged;

        return Result.Success();
    }
}
