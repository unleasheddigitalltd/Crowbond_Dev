using Crowbond.Modules.OMS.Domain.PurchaseOrders;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderHistory;

public sealed record PurchaseOrderHistoryResponse(Guid Id, Guid PurchaseOrderHeaderId, int Status, DateTime ChangedAt, Guid ChangedBy);
