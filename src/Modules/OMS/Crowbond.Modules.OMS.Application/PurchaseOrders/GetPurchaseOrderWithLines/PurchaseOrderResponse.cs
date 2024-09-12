namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderWithLines;

public sealed record PurchaseOrderResponse
{
    public PurchaseOrderResponse()
    {
        Lines = new List<PurchaseOrderLineResponse>();
    }

    public Guid Id { get; }
    public string? PurchaseOrderNo { get; }
    public DateOnly? PurchaseDate { get; }
    public string SupplierName { get; }
    public string? ContactFullName { get; }
    public string? ContactPhone { get; }
    public string? ContactEmail { get; }
    public DateOnly RequiredDate { get; }
    public decimal PurchaseOrderAmount { get; }
    public string? PurchaseOrderNotes { get; }
    public Guid CreatedBy { get; }
    public DateTime CreatedOnUtc { get; }
    public List<PurchaseOrderLineResponse> Lines { get; set; }
}
public sealed record PurchaseOrderLineResponse(
    Guid Id,
    Guid PurchaseOrderHeaderId,
    Guid ProductId,
    decimal Qty,
    decimal UnitPrice);
