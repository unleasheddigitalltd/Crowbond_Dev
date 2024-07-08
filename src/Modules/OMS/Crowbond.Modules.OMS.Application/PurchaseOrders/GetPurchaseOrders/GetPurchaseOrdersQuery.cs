using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.GetPurchaseOrders;

public sealed record GetPurchaseOrdersQuery(string Search, string Sort, string Order, int Page, int Size) : IQuery<PurchaseOrdersResponse>;
