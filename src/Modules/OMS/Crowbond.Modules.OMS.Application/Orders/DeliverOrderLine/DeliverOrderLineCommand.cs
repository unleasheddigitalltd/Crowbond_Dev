using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.DeliverOrderLine;

public sealed record DeliverOrderLineCommand(
    Guid OrderLineId,
    Guid DriverId, 
    decimal DeliveredQty,
    Guid? RejectReasonId, 
    string? DeliveryComments) : ICommand;
