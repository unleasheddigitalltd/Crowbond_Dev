﻿using Crowbond.Modules.CRM.Domain.Customers;
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
     ShowLogoInDeliveryDocket showLogoInDeliveryDocket,
     string? customerLogo,
     bool IsActive,
     List<CustomerRequest.CustomerContact> CustomerContacts,
     List<CustomerRequest.CustomerOutletAddress> CustomerOutletAddresses
    )
{
    public sealed record CustomerContact(
        Guid Id,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Mobile,
        string Email,
        bool Primary,
        bool ReceiveInvoice,
        bool ReceiveOrder,
        bool ReceivePriceList,
        bool IsActive);

    public sealed record CustomerOutletAddress(
        Guid Id,
        string LocationName,
        string FullName,
        string? Email,
        string PhoneNumber,
        string? Mobile,
        string AddressLine1,
        string? AddressLine2,
        string TownCity,
        string County,
        string? Country,
        string PostalCode,
        string? DeliveryNote,
        DateTime DeliveryTimeFrom,
        DateTime DeliveryTimeTo,
        bool Is24HrsDelivery);
};
