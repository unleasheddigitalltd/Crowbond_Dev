using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.GetMyOrders;

public sealed record GetMyOrdersQuery(
    Guid CustomerContactId,
    string Search,
    string Sort,
    string Order,
    int Page,
    int Size)
    : IQuery<OrdersResponse>;
