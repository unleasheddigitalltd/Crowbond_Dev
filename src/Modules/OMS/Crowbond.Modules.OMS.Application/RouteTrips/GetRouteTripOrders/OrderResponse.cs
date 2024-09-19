using Crowbond.Modules.OMS.Domain.Payments;

namespace Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTripOrders;
public sealed record OrderResponse(
    Guid Id,
    string OrderNumber,
    Guid CustomerId,
    string CustomerBusinessName,
    string DeliveryLocationName,
    string DeliveryFullName,
    string DeliveryPhone,
    string DeliveryMobile,
    string DeliveryNotes,
    string DeliveryAddressLine1,
    string DeliveryAddressLine2,
    string DeliveryTownCity,
    string DeliveryCounty,
    string DeliveryPostalCode,
    DateOnly ShippingDate,
    Guid RouteTripId,
    string RouteName,
    decimal OrderAmount,
    PaymentMethod PaymentMethod,
    string CustomerComment)

{
    public List<OrderLineResponse> OrderLines { get; set; } = [];
}


