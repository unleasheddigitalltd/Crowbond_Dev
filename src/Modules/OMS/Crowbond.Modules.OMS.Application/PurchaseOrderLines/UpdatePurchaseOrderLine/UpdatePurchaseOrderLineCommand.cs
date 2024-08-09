using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrderLines.UpdatePurchaseOrderLine;

public sealed record UpdatePurchaseOrderLineCommand(Guid PurchaseOrderLineId, decimal Qty, string? Comments) : ICommand;
