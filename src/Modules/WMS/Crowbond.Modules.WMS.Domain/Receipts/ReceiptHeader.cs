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

    public DateTime ReceivedDate { get; private set; }

    public Guid? PurchaseOrderId { get; private set; }

    public string? PurchaseOrderNo { get; private set; }

    public string DeliveryNoteNumber { get; private set; }

    public ReceiptStatus Status { get; private set; }

    public Guid CreatedBy { get; private set; }

    public DateTime CreatedDate { get; private set; }

    public Guid? LastModifiedBy { get; private set; }

    public DateTime? LastModifiedDate { get; private set; }

    public IReadOnlyCollection<ReceiptLine> Lines => _lines;

    public static ReceiptHeader Create(
        string receiptNo,
        DateTime receivedDate,
        Guid purchaseOrderId,
        string? purchaseOrderNumber,
        string deliveryNoteNumber,
        Guid createdBy,
        DateTime createdDate)
    {
        var receiptHeader = new ReceiptHeader
        {
            Id = Guid.NewGuid(),
            ReceiptNo = receiptNo,
            ReceivedDate = receivedDate,
            PurchaseOrderId = purchaseOrderId,
            PurchaseOrderNo = purchaseOrderNumber,
            DeliveryNoteNumber = deliveryNoteNumber,
            Status = ReceiptStatus.Shipping,
            CreatedBy = createdBy,
            CreatedDate = createdDate
        };

        return receiptHeader;
    }

    public Result Receive(Guid lastModifiedBy, DateTime lastModifiedDate)
    {
        if (Status != ReceiptStatus.Shipping)
        {
            return Result.Failure(ReceiptErrors.NotShipping);
        }

        Status = ReceiptStatus.Received;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;

        return Result.Success();
    }

    public Result Cancel(Guid lastModifiedBy, DateTime lastModifiedDate)
    {
        if (Status != ReceiptStatus.Shipping)
        {
            return Result.Failure(ReceiptErrors.NotShipping);
        }

        Status = ReceiptStatus.Cancelled;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;

        return Result.Success();
    }

    public Result<ReceiptLine> AddLine(
        Guid modifiedBy, 
        DateTime modiriedDate, 
        Guid productId,
        decimal quantityReceived,
        decimal unitPrice)
    {
        var receiptLine = ReceiptLine.Create(productId, quantityReceived, unitPrice);

        _lines.Add(receiptLine);
        LastModifiedBy = modifiedBy;
        LastModifiedDate = modiriedDate;

        return Result.Success(receiptLine);
    }
}
