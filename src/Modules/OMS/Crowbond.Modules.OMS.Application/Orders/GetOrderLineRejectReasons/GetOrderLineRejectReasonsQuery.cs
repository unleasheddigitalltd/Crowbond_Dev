using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrderLineRejectReasons;

public sealed record GetOrderLineRejectReasonsQuery(): IQuery<IReadOnlyCollection<RejectReasonResponse>>;
