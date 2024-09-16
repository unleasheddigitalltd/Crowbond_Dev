using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Carts.AddItemToCart;

public sealed record AddItemToCartCommand(Guid ContactId, Guid ProductId, decimal Quantity)
    : ICommand;
