using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.PendPurchaseOrder;

public sealed record PendPurchaseOrderCommand(Guid PurchaseOrderHeaderId) : ICommand;
