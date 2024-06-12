using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Receipts;

public sealed class ReceiptHeader : Entity
{
    public ReceiptHeader()
    {
    }

    public Guid Id { get; private set; }

    public DateTime ReceivedDate { get; private set; }

    public Guid PurchaseOrderId { get; private set; }

    public string DeivaryNoteNumber { get; private set; }

    public ReceiptStatus Status { get; private set; }

    public DateTime CreatetimeStamp { get; private set; }

    public static ReceiptHeader Create(
        DateTime receivedDate,
        Guid purchaseOrderId,
        string deliveryNoteNumber,
        DateTime createtimeStamp)
    {
        var receiptHeader = new ReceiptHeader
        {
            Id = Guid.NewGuid(),
            ReceivedDate = receivedDate,
            PurchaseOrderId = purchaseOrderId,
            DeivaryNoteNumber = deliveryNoteNumber,
            Status = ReceiptStatus.Shipping,
            CreatetimeStamp = createtimeStamp
        };

        return receiptHeader;
    }
}
