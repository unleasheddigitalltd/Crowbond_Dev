using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Ticketing.Application.Carts.GetCart;

public sealed record GetCartQuery(Guid CustomerId) : IQuery<Cart>;
