using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrderLine;

public sealed record GetOrderLineQuery(Guid OrderLineId) : IQuery<OrderLineResponse>;
