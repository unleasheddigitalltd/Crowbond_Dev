using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderLines;

public sealed record GetPurchaseOrderLinesQuery(Guid PurchaseOrderHeaderId) : IQuery<IReadOnlyCollection<PurchaseOrderLineResponse>>;
