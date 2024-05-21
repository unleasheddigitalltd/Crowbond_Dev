using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Ticketing.Application.Carts.RemoveItemFromCart;

public sealed record RemoveItemFromCartCommand(Guid CustomerId, Guid TicketTypeId) : ICommand;
