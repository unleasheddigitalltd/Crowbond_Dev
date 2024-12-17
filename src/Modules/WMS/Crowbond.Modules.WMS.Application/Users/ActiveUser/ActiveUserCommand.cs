using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Users.ActiveUser;

public sealed record ActiveUserCommand(Guid UserId) : ICommand;
