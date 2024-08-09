using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrder;

public sealed record GetPurchaseOrderQuery(Guid PurchaseOrderHeaderId) : IQuery<PurchaseOrderResponse>;

