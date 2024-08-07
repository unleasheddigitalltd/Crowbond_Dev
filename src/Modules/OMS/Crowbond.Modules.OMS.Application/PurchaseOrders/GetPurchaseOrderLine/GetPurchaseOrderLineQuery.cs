using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderLines;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderLine;

public sealed record GetPurchaseOrderLineQuery(Guid PurchaseOrderLineId) : IQuery<PurchaseOrderLineResponse>;
