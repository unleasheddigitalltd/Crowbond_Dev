using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.GetMyOrder;

public sealed record GetMyOrderQuery(Guid CustomerContactId, Guid OrderHeaderId) : IQuery<OrderResponse>;
