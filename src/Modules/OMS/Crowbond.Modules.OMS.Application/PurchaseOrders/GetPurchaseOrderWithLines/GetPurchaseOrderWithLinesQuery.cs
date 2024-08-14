using System;
using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrderWithLines;

public sealed record GetPurchaseOrderWithLinesQuery(Guid PurchaseOrderHeaderId) : IQuery<PurchaseOrderResponse>;
