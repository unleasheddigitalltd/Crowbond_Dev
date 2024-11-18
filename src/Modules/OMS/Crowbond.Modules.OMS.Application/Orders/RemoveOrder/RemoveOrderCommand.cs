using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.RemoveOrder;

public sealed record RemoveOrderCommand(Guid OrderId) :ICommand;
