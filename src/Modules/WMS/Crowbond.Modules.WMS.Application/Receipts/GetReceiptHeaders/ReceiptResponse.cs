using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.WMS.Application.Receipts.GetReceiptHeaders;

public sealed class ReceiptResponse : PaginatedResponse<ReceiptHeader>
{
    public ReceiptResponse(IReadOnlyCollection<ReceiptHeader> receipts, IPagination pagination)
        : base(receipts, pagination)
    {
    }
}

public sealed record ReceiptHeader
{
    public Guid Id { get; }
    public string ReceiptNo { get; }
    public DateTime? ReceivedDate { get; }
    public string? PurchaseOrderNo { get; }
    public string? DeliveryNoteNumber { get; }
    public string Status { get; }
    public DateTime CreatedOnUtc { get; }
    public string? SupplierName { get; }
}
