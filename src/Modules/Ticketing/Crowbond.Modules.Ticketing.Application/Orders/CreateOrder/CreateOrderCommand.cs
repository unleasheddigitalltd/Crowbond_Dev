using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Ticketing.Application.Orders.CreateOrder;

public sealed record CreateOrderCommand(Guid CustomerId) : ICommand;
