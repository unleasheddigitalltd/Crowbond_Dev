using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Carts.RemoveItemFromCart;

public sealed record RemoveItemFromCartCommand(Guid ContactId, Guid ProductId) : ICommand;
