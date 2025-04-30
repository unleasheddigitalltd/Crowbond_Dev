using Crowbond.Modules.OMS.Application.Orders.GetOrder;

namespace Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTripsWithPendingOrders;

public sealed record RouteTripWithPendingOrdersResponse(
    Guid Id,
    string RouteName,
    int Position,
    int Status,
    DateTime Date,
    string? Comments,
    List<OrderResponse> Orders);
