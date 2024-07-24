using Crowbond.Modules.OMS.Domain.Payments;
using Crowbond.Modules.OMS.Domain.PurchaseOrders;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.CreatePurchaseOrder;


public sealed record PurchaseOrderRequest(
    string PaidBy,
    DateTime? PaidDate,
    Guid SupplierId,
    string SupplierName,
    string SupplierPhone,
    string SupplierEmail,
    string SupplierContact,
    decimal PurchaseOrderAmount,
     string ShippingAddressLine1,
     string? ShippingAddressLine2,
     string ShippingAddressTownCity,
     string ShippingAddressCounty,
     string? ShippingAddressCountry,
     string ShippingAddressPostalCode,
    DateOnly RequiredDate,
    DateOnly PurchaseDate,
    DateOnly ExpectedShippingDate,
    string? SupplierReference,
    decimal PurchaseOrderTax,
    DeliveryMethod DeliveryMethod,
    decimal DeliveryCharge,
    PaymentMethod PaymentMethod,
    PaymentStatus PaymentStatus,
    string? PurchaseOrderNotes,
    string SalesOrderRef,
    string Tags
    );



