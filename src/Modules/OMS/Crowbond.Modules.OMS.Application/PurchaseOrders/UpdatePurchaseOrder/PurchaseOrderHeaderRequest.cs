using Crowbond.Modules.OMS.Domain.Payments;
using Crowbond.Modules.OMS.Domain.PurchaseOrderHeaders;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.UpdatePurchaseOrder;

public sealed record PurchaseOrderHeaderRequest(
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
    DateOnly? ExpectedShippingDate,
    string? SupplierReference,
    DeliveryMethod? DeliveryMethod,
    decimal DeliveryCharge,
    PaymentMethod PaymentMethod,
    string? PurchaseOrderNotes,
    string? SalesOrderRef);
