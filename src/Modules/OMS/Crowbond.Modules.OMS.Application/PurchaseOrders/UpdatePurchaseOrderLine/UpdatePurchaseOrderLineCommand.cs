using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.UpdatePurchaseOrderLine;

public sealed record UpdatePurchaseOrderLineCommand(
    Guid PurchaseOrderLineId,
    decimal UnitPrice,
    decimal Qty,
    string? Comments) : ICommand;
