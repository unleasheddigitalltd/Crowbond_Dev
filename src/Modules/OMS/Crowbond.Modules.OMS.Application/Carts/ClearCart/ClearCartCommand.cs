using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Carts.ClearCart;

public sealed record ClearCartCommand(Guid ContactId) : ICommand;
