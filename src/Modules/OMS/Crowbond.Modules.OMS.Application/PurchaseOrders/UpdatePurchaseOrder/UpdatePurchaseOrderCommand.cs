using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.UpdatePurchaseOrder;

public sealed record UpdatePurchaseOrderCommand(Guid PurchaseOrderHeaderId, PurchaseOrderRequest PurchaseOrder) : ICommand;
