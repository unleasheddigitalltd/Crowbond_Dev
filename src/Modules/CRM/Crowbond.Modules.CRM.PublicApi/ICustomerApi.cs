﻿namespace Crowbond.Modules.CRM.PublicApi;

public interface ICustomerApi
{
    Task<CustomerForOrderResponse?> GetForOrderAsync(Guid contactId, CancellationToken cancellationToken = default);

    Task<CustomerOutletForOrderResponse?> GetOutletForOrderAsync(Guid outletId, CancellationToken cancellationToken = default);
}

public sealed record CustomerForOrderResponse(
    Guid Id,
    string AccountNumber,
    string BusinessName,
    Guid PriceTierId,
    decimal Discount,
    bool NoDiscountSpecialItem,
    bool NoDiscountFixedPrice,
    bool DetailedInvoice,
    int PaymentTerms,
    string? CustomerNotes,
    int DeliveryFeeSetting,
    decimal DeliveryMinOrderValue,
    decimal DeliveryCharge);

public sealed record CustomerOutletForOrderResponse( 
    Guid Id,
    Guid CustomerId,
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
    TimeOnly DeliveryTimeFrom,
    TimeOnly DeliveryTimeTo,
    bool Is24HrsDelivery);
