﻿namespace Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutletForOrder;

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
