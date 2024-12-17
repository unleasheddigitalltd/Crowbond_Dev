using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Users.DeactiveUser;

public sealed record DeactiveUserCommand(Guid UserId) : ICommand;
