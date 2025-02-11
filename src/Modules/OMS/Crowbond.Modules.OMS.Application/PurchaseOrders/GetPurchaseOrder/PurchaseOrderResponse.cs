namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrder;

public sealed record PurchaseOrderResponse(
    Guid Id,
    string? PurchaseOrderNo,
    DateOnly? PurchaseDate,
    Guid SupplierId,
    string SupplierName,
    string? ContactFullName,
    string? ContactPhone,
    string? ContactEmail,
    string? ShippingLocationName,
    string? ShippingAddressLine1,
    string? ShippingAddressLine2,
    string? ShippingTownCity,
    string? ShippingCounty,
    string? ShippingCountry,
    string? ShippingPostalCode,
    DateOnly RequiredDate,
    decimal PurchaseOrderAmount,
    int PaymentStatus,
    string? PurchaseOrderNotes,
    int Status);

