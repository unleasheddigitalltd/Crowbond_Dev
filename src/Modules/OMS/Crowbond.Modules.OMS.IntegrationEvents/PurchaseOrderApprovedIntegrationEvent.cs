using Crowbond.Common.Application.EventBus;

namespace Crowbond.Modules.OMS.IntegrationEvents;

public sealed class PurchaseOrderApprovedIntegrationEvent : IntegrationEvent
{
    public PurchaseOrderApprovedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid purchaseOrderId,
        string? purchaseOrderNo,
        List<ReceiptLine> receiptLines)
        : base(id, occurredOnUtc)
    {
        PurchaseOrderId = purchaseOrderId;
        PurchaseOrderNo = purchaseOrderNo;
        ReceiptLines = receiptLines;
    }

    public Guid PurchaseOrderId { get; init; }

    public string? PurchaseOrderNo { get; init; }

    public List<ReceiptLine> ReceiptLines { get; init; }

    public sealed record ReceiptLine(
        Guid ProductId,
        decimal Qty,
        decimal UnitPrice);

}

