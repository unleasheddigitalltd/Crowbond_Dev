using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Receipts;

public sealed class ReceiptHeader : Entity
{
    public ReceiptHeader()
    {
    }

    public Guid Id { get; private set; }

    public DateTime ReceivedDate { get; private set; }

    public Guid? PurchaseOrderId { get; private set; }

    public string? PurchaseOrderNo { get; private set; }

    public string DeliveryNoteNumber { get; private set; }

    public ReceiptStatus Status { get; private set; }

    public DateTime CreatetimeStamp { get; private set; }

    public static ReceiptHeader Create(
        DateTime receivedDate,
        Guid purchaseOrderId,
        string? purchaseOrderNumber,
        string deliveryNoteNumber,
        DateTime createtimeStamp)
    {
        var receiptHeader = new ReceiptHeader
        {
            Id = Guid.NewGuid(),
            ReceivedDate = receivedDate,
            PurchaseOrderId = purchaseOrderId,
            PurchaseOrderNo = purchaseOrderNumber,
            DeliveryNoteNumber = deliveryNoteNumber,
            Status = ReceiptStatus.Shipping,
            CreatetimeStamp = createtimeStamp
        };

        return receiptHeader;
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
}
