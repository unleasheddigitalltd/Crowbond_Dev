using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrderLines.RemovePurchaseOrderLine;

public sealed record RemovePurchaseOrderLineCommand(Guid PurchaseOrderLineId) : ICommand;
