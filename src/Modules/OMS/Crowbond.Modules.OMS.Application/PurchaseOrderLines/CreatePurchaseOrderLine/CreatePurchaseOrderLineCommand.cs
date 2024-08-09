using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrderLines.CreatePurchaseOrderLine;

public sealed record CreatePurchaseOrderLineCommand(Guid PurchaseOrderHeaderId, Guid ProductId, decimal Qty, string? Comments) : ICommand<Guid>;
