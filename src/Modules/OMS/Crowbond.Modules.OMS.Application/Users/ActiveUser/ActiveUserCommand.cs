using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Users.ActiveUser;

public sealed record ActiveUserCommand(Guid UserId): ICommand;
