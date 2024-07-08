namespace Crowbond.Modules.CRM.Application.PurchaseOrders.CreatePurchaseOrder;

public sealed record PurchaseOrderResponse(
     Guid Id,
     string SupplierName,
     string AddressLine1, 
     string? AddressLine2,
     string AddressTownCity,
     string AddressCounty,
     string? AddressCountry,
     string AddressPostalCode,
     string BillingAddressLine1,
     string? BillingAddressLine2,
     string BillingAddressTownCity,
     string BillingAddressCounty, 
     string BillingAddressCountry,
     string BillingAddressPostalCode,
     int PaymentTerms,
     string? SupplierNotes,
    string SupplierEmail,
    string SupplierPhone,
    string SupplierContact
    );
