namespace Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutletDetails;
public sealed record CustomerOutletDetailsResponse(
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
    bool Is24HrsDelivery);
