namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderWithLines;

public sealed record PurchaseOrderResponse(
    Guid Id,
    string? PurchaseOrderNo,
    DateOnly? PurchaseDate,
    string SupplierName,
    string? ContactFullName,
    string? ContactPhone,
    string? ContactEmail,
    DateOnly RequiredDate,
    decimal PurchaseOrderAmount,
    string? PurchaseOrderNotes,
    Guid CreatedBy,
    DateTime CreatedOnUtc)
{
    public List<PurchaseOrderLineResponse> Lines { get; } = [];
}

