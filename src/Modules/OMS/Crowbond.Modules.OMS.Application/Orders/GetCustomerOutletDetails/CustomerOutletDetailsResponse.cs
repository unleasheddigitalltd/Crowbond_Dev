namespace Crowbond.Modules.OMS.Application.Orders.GetCustomerOutletDetails;

public sealed record CustomerOutletDetailsResponse(
    Guid CustomerId,
    Guid CustomerOutletId,
    string LocationName,
    string AddressLine1,
    string AddressLine2,
    string TownCity,
    string County,
    string PostalCode,
    string? DeliveryNotes);
