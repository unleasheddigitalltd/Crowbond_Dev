namespace Crowbond.Modules.CRM.PublicApi;

public interface ICustomerApi
{
    Task<CustomerForOrderResponse?> GetByContactIdAsync(Guid contactId, CancellationToken cancellationToken = default);

    Task<CustomerForOrderResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<CustomerOutletForOrderResponse?> GetOutletForOrderAsync(Guid outletId, CancellationToken cancellationToken = default);

    Task<CustomerOutletRouteResponse?> GetOutletRouteForDayAsync(Guid outletId, DayOfWeek weekday, CancellationToken cancellationToken = default);
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
    int DueDateCalculationBasis,
    int DueDaysForInvoice,
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

public sealed record CustomerOutletRouteResponse(
    Guid RouteId,
    string RouteName);
