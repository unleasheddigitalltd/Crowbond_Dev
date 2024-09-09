using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.CancelPurchaseOrder;

public sealed record CancelPurchaseOrderCommand(Guid PurchaseOrderHeaderId) : ICommand;
