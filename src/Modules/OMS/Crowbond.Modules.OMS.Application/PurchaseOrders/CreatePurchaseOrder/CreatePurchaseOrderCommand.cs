using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.CreatePurchaseOrder;

public sealed record CreatePurchaseOrderCommand(Guid UserId, PurchaseOrderRequest PurchaseOrder) : ICommand<Guid>;
