using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Users.Application.Users.AssignRole;

public sealed record AssignRoleCommand(Guid UserId, string RoleName) : ICommand;
