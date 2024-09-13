using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.UpdatePurchaseOrderDetails;

public sealed record UpdatePurchaseOrderDetailsCommand(Guid PurchaseOrderHeaderId, PurchaseOrderDetailsRequest PurchaseOrderHeader) : ICommand;
