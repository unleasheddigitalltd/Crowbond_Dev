using System.Runtime.InteropServices;
using Crowbond.Common.Domain;

namespace Crowbond.Modules.Warehouse.Domain.Stocks;

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

    public DateTime? SellByDate { get;  private set; }

    public DateTime? UseByDate { get; private set; }

    public Guid ReceiptId { get; private set; }

    public string? Note { get; private set; }

    public StockStatus Status { get; private set; }

    public static Stock Create(
        Guid productId,
        Guid locationId,
        decimal originalQty,
        decimal currentQty,
        string batchNumber,
        DateTime receivedDate,
        DateTime? sellByDate,
        DateTime? useByDate,
        Guid receiptId,
        string? note)
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
            ReceiptId = receiptId,
            Note = note,
            Status = StockStatus.Active
        };

        return stock;
    }
}
