using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderHistory;

public sealed record GetPurchaseOrderHistoryQuery(Guid PurchaseOrderHeaderId) : IQuery<IReadOnlyCollection<PurchaseOrderHistoryResponse>>;
