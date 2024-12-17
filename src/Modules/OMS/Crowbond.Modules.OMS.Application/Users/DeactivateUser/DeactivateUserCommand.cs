using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Users.DeactivateUser;

public sealed record DeactivateUserCommand(Guid UserId) : ICommand;
