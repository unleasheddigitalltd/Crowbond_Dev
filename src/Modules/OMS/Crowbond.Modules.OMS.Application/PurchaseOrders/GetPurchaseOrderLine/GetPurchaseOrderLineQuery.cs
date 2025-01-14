using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderLine;

public sealed record GetPurchaseOrderLineQuery(Guid PurchaseOrderLineId) : IQuery<PurchaseOrderLineResponse>;
