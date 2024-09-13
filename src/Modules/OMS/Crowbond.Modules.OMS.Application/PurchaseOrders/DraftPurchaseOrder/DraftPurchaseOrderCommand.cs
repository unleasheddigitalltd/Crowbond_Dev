using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.DraftPurchaseOrder;

public sealed record DraftPurchaseOrderCommand(Guid PurchaseOrderHeaderId) : ICommand;
