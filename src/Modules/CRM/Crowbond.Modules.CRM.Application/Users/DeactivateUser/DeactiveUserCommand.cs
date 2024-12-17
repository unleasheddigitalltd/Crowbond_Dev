using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Users.DeactivateUser;

public sealed record DeactiveUserCommand(Guid UserId) : ICommand;
