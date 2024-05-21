using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Ticketing.Application.Carts.ClearCart;

public sealed record ClearCartCommand(Guid CustomerId) : ICommand;
