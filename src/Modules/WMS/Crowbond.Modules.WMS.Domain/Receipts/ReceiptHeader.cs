using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Receipts;

public sealed class ReceiptHeader : Entity
{
    private readonly List<ReceiptLine> _lines = new();

    private ReceiptHeader()
    {
    }

    public Guid Id { get; private set; }

    public string ReceiptNo { get; private set; }

    public DateOnly? ReceivedDate { get; private set; }

    public Guid? PurchaseOrderId { get; private set; }

    public string? PurchaseOrderNo { get; private set; }

    public string? DeliveryNoteNumber { get; private set; }

    public ReceiptStatus Status { get; private set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOnUtc { get; set; }

    public IReadOnlyCollection<ReceiptLine> Lines => _lines;

    public static ReceiptHeader Create(
        string receiptNo,
        Guid purchaseOrderId,
        string? purchaseOrderNumber)
    {
        var receiptHeader = new ReceiptHeader
        {
            Id = Guid.NewGuid(),
            ReceiptNo = receiptNo,
            PurchaseOrderId = purchaseOrderId,
            PurchaseOrderNo = purchaseOrderNumber,
            Status = ReceiptStatus.Shipping
        };

        return receiptHeader;
    }

    public Result Receive(DateTime utcNow)
    {
        if (Status != ReceiptStatus.Shipping)
        {
            return Result.Failure(ReceiptErrors.NotShipping);
        }

        ReceivedDate = DateOnly.FromDateTime(utcNow);
        Status = ReceiptStatus.Received;

        return Result.Success();
    }

    public Result Cancel(Guid userId, DateTime utcNow)
    {
        if (Status != ReceiptStatus.Shipping)
        {
            return Result.Failure(ReceiptErrors.NotShipping);
        }

        Status = ReceiptStatus.Cancelled;
        LastModifiedBy = userId;
        LastModifiedOnUtc = utcNow;
        return Result.Success();
    }

    public Result<ReceiptLine> AddLine(
        Guid productId,
        decimal quantityReceived,
        decimal unitPrice)
    {
        var receiptLine = ReceiptLine.Create(productId, quantityReceived, unitPrice);

        _lines.Add(receiptLine);

        return Result.Success(receiptLine);
    }
}
