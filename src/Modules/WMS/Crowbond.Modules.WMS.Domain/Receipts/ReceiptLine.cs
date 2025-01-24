using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Receipts;

public sealed class ReceiptLine : Entity
{
    private ReceiptLine()
    {
    }

    public Guid Id { get; private set; }

    public Guid ReceiptHeaderId { get; private set; }

    public Guid ProductId { get; private set; }

    public decimal ReceivedQty { get; private set; }

    public decimal StoredQty { get; private set; }

    public decimal MissedQty { get; private set; }

    public decimal UnitPrice { get; private set; }

    public DateOnly? SellByDate { get; private set; }

    public DateOnly? UseByDate { get; private set; }

    public string BatchNumber { get; private set; }

    public bool IsStored { get; private set; }

    internal static ReceiptLine Create(
        Guid productId,
        decimal receivedQty,
        decimal unitPrice)
    {
        var receiptLine = new ReceiptLine
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            ReceivedQty = receivedQty,
            StoredQty = 0,
            MissedQty = 0,
            UnitPrice = unitPrice,
            BatchNumber = GenerateBatchNumber(),
            IsStored = false
        };

        return receiptLine;
    }

    internal Result Update(
        decimal receivedQty,
        decimal unitPrice)
    {
        if (IsStored)
        {
            return Result.Failure(ReceiptErrors.LineAlreadyStored);    
        }

        ReceivedQty = receivedQty;
        UnitPrice = unitPrice;

        return Result.Success();
    }

    internal Result Store(decimal qty)
    {
        if (IsStored)
        {
            return Result.Failure(ReceiptErrors.LineAlreadyStored);
        }

        StoredQty += qty;

        IsStored = StoredQty == ReceivedQty;

        return Result.Success();
    }

    internal Result FinalizeStorage()
    {
        MissedQty = ReceivedQty - StoredQty;
        IsStored = true;

        return Result.Success();
    }

    private static string GenerateBatchNumber()
    {
        string batchNumber = "";
        return batchNumber;
    }
}
