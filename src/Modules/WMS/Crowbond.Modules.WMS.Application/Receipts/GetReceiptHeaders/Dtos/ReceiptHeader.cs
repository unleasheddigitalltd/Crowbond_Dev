using Crowbond.Modules.WMS.Domain.Receipts;

namespace Crowbond.Modules.WMS.Application.Receipts.GetReceipts.Dtos;

public sealed record ReceiptHeader
{
    public Guid Id { get; }

    public DateTime ReceivedDate { get; }

    public string PurchaseOrderNumber { get; }

    public string DeliveryNoteNumber { get; }

    public string Status { get; }
}
