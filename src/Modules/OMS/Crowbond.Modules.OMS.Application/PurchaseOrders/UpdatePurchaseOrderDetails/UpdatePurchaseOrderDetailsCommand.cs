using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.PurchaseOrders.UpdatePurchaseOrderDetails;

public sealed record UpdatePurchaseOrderDetailsCommand(Guid PurchaseOrderHeaderId, Guid UserId, PurchaseOrderDetailsRequest PurchaseOrderHeader) : ICommand;
