using Crowbond.Modules.OMS.Domain.Orders;

namespace Crowbond.Modules.OMS.Application.RouteTrips.GetRouteTripOrders;

public sealed record OrderLineResponse(
        Guid OrderLineId,
        Guid OrderHeaderId,
        Guid ProductId,
        string ProductSku,
        string ProductName,
        decimal OrderedQty,
        decimal ActualQty,
        decimal DeliveredQty,
        OrderLineStatus Status);
