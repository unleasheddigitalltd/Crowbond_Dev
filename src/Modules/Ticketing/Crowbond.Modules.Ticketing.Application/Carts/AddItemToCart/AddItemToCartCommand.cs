using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Ticketing.Application.Carts.AddItemToCart;

public sealed record AddItemToCartCommand(Guid CustomerId, Guid TicketTypeId, decimal Quantity)
    : ICommand;
