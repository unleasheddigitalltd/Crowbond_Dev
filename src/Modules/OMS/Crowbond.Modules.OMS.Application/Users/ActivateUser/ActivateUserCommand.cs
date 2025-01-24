using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Users.ActivateUser;

public sealed record ActivateUserCommand(Guid UserId) : ICommand;
