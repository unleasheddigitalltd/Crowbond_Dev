using Crowbond.Modules.OMS.Domain.Orders;

namespace Crowbond.Modules.OMS.Application.Orders.GetOrderLineRejectReasons;

public sealed record RejectReasonResponse(Guid Id, string Title, OrderLineRejectResponsibility Responsibility, bool IsActive);
