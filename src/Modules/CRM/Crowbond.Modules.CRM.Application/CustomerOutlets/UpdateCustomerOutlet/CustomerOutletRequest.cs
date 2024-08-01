namespace Crowbond.Modules.CRM.Application.CustomerOutlets.UpdateCustomerOutlet;

public sealed record CustomerOutletRequest(
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
