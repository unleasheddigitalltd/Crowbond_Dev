using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Receipts;

public sealed class ReceiptHeader : Entity , IAuditable
{
    private readonly List<ReceiptLine> _lines = new();

    private ReceiptHeader()
    {
    }

    public Guid Id { get; private set; }

    public string ReceiptNo { get; private set; }

    public DateTime ReceivedDate { get; private set; }

    public Guid? PurchaseOrderId { get; private set; }

    public string? PurchaseOrderNo { get; private set; }

    public string DeliveryNoteNumber { get; private set; }

    public ReceiptStatus Status { get; private set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime? LastModifiedOnUtc { get; set; }

    public IReadOnlyCollection<ReceiptLine> Lines => _lines;

    public static ReceiptHeader Create(
        string receiptNo,
        DateTime receivedDate,
        Guid purchaseOrderId,
        string? purchaseOrderNumber,
        string deliveryNoteNumber,
        Guid createdBy,
        DateTime createdOnUtc)
    {
        var receiptHeader = new ReceiptHeader
        {
            Id = Guid.NewGuid(),
            ReceiptNo = receiptNo,
            ReceivedDate = receivedDate,
            PurchaseOrderId = purchaseOrderId,
            PurchaseOrderNo = purchaseOrderNumber,
            DeliveryNoteNumber = deliveryNoteNumber,
            CreatedBy = createdBy,
            CreatedOnUtc = createdOnUtc,
            Status = ReceiptStatus.Shipping
        };

        return receiptHeader;
    }

    public Result Receive()
    {
        if (Status != ReceiptStatus.Shipping)
        {
            return Result.Failure(ReceiptErrors.NotShipping);
        }

        Status = ReceiptStatus.Received;

        return Result.Success();
    }

    public Result Cancel()
    {
        if (Status != ReceiptStatus.Shipping)
        {
            return Result.Failure(ReceiptErrors.NotShipping);
        }

        Status = ReceiptStatus.Cancelled;

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
