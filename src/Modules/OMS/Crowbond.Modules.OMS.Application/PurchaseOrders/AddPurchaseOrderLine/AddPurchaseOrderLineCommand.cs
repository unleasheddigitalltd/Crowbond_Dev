using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.AddPurchaseOrderLine;

public sealed record AddPurchaseOrderLineCommand(
    Guid PurchaseOrderId,
    Guid ProductId, 
    decimal UnitPrice,
    decimal Qty, 
    string? Comments) : ICommand<Guid>;
