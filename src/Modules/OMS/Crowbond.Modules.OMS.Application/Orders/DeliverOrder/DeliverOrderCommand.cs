using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.DeliverOrder;

public sealed record DeliverOrderCommand(Guid OrderHeaderId, Guid DriverId, string? Comments) : ICommand<Guid>;
