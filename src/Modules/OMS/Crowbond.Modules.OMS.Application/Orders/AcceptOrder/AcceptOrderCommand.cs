using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Orders.AcceptOrder;

public sealed record AcceptOrderCommand(Guid OrderId) : ICommand;
