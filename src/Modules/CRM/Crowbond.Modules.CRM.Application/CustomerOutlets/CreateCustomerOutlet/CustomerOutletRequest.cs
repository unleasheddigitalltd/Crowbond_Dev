namespace Crowbond.Modules.CRM.Application.CustomerOutlets.CreateCustomerOutlet;

public sealed record CustomerOutletRequest(
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
    string DeliveryTimeFrom,
    string DeliveryTimeTo,
    bool Is24HrsDelivery);
