namespace Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTripOrders;

public sealed record OrderLineResponse(
        Guid OrderLineId,
        Guid OrderHeaderId,
        Guid ProductId,
        string ProductSku,
        string ProductName,
        decimal Qty);
