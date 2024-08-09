using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.OMS.Application.PurchaseOrderLines.GetPurchaseOrderLines;

namespace Crowbond.Modules.OMS.Application.PurchaseOrderLines.GetPurchaseOrderLine;

public sealed record GetPurchaseOrderLineQuery(Guid PurchaseOrderLineId) : IQuery<PurchaseOrderLineResponse>;
