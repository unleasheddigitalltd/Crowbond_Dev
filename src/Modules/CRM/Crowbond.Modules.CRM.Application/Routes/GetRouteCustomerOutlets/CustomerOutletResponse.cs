﻿namespace Crowbond.Modules.CRM.Application.Routes.GetRouteCustomerOutlets;

public sealed record CustomerOutletResponse(
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
    bool Is24HrsDelivery)
{
    public List<CustomerOutletRouteResponse> Routes { get; set; }
};
