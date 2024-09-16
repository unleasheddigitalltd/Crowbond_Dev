using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Carts.GetCart;

public sealed record GetCartQuery(Guid ContactId) : IQuery<Cart>;
