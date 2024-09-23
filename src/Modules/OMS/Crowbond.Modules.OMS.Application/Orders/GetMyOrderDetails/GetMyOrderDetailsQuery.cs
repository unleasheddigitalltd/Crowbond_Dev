using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.GetMyOrderDetails;

public sealed record GetMyOrderDetailsQuery(Guid CustomerContactId, Guid OrderHeaderId) : IQuery<OrderResponse>;
