namespace Crowbond.Modules.OMS.Application.PurchaseOrders.CreatePurchaseOrder;

public sealed record PurchaseOrderRequest(
    Guid SupplierId,
    DateOnly RequiredDate,
    string? PurchaseOrderNotes);


