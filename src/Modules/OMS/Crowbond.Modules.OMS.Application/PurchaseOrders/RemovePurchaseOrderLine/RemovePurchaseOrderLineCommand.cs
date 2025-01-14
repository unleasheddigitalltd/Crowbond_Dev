using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.RemovePurchaseOrderLine;

public sealed record RemovePurchaseOrderLineCommand(Guid PurchaseOrderLineId) : ICommand;
