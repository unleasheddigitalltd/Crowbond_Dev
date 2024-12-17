using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Users.DeactiveUser;

public sealed record DeactiveUserCommand(Guid UserId) : ICommand;
