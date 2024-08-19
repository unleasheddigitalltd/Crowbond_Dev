using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Users.Application.Users.ActivateUser;

public sealed record ActivateUserCommand(Guid UserId) : ICommand;
