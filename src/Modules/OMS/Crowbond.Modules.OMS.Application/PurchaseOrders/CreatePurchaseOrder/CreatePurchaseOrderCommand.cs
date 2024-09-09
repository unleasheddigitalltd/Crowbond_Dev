using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.CreatePurchaseOrder;

public sealed record CreatePurchaseOrderCommand(PurchaseOrderRequest PurchaseOrder) : ICommand<Guid>;
