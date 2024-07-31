namespace Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutlets;
public sealed record CustomerOutletResponse(
    Guid Id,
    Guid CustomerId,
    string LocationName,
    string AddressLine1,
    string? AddressLine2,
    string TownCity,
    string County,
    string? Country,
    string PostalCode);
