namespace Crowbond.Modules.CRM.Application.Customers.CreateCustomer;

public sealed record CustomerRequest(
     int AccountNumber,
     string BusinessName,
     string? DriverCode,
     string ShippingAddressLine1,
     string? ShippingAddressLine2,
     string ShippingTownCity,
     string ShippingCounty,
     string? ShippingCountry,
     string ShippingPostalCode,
     string BillingAddressLine1,
     string? BillingAddressLine2,
     string BillingTownCity,
     string BillingCounty,
     string BillingCountry,
     string BillingPostalCode,
     int PriceGroupId,
     Guid InvoicePeriodId,
     int PaymentTerms,
     bool DetailedInvoice,
     string? CustomerNotes,
     string CustomerEmail,
     string CustomerPhone,
     string CustomerContact
    );
