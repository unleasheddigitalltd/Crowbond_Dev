using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrderLines;

public sealed record GetOrderLinesQuery(Guid OrderHeaderId) : IQuery<IReadOnlyCollection<OrderLineResponse>>;
