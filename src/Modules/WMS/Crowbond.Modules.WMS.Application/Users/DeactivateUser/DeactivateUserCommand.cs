using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Users.DeactivateUser;

public sealed record DeactivateUserCommand(Guid UserId) : ICommand;
