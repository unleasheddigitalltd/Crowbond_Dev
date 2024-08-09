using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrderLines.GetPurchaseOrderLines;

public sealed record GetPurchaseOrderLinesQuery(Guid PurchaseOrderHeaderId) : IQuery<IReadOnlyCollection<PurchaseOrderLineResponse>>;
