using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Users.Application.Users.DeactivateUser;

public sealed record DeactivateUserCommand(Guid UserId) : ICommand;
