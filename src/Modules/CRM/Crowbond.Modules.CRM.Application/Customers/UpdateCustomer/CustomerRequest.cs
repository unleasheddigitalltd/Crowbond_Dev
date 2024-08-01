using Crowbond.Modules.CRM.Domain.Customers;
using Crowbond.Modules.CRM.Domain.CustomerSettings;

namespace Crowbond.Modules.CRM.Application.Customers.UpdateCustomer;

public sealed record CustomerRequest(
     string BusinessName,
     string BillingAddressLine1,
     string? BillingAddressLine2,
     string BillingTownCity,
     string BillingCounty,
     string BillingCountry,
     string BillingPostalCode,
     Guid PriceTierId,
     decimal Discount,
     Guid? RepId,
     bool CustomPaymentTerm,
     PaymentTerm PaymentTerms,
     int? InvoiceDueDays,
     DeliveryFeeSetting DeliveryFeeSetting,
     decimal? DeliveryMinOrderValue,
     decimal? DeliveryCharge,
     bool NoDiscountSpecialItem,
     bool NoDiscountFixedPrice,
     bool DetailedInvoice,
     string? CustomerNotes,
     bool IsHq,
     bool ShowPricesInDeliveryDocket,
     bool ShowPriceInApp,
     ShowLogoInDeliveryDocket ShowLogoInDeliveryDocket,
     string? CustomerLogo,
     bool IsActive);
