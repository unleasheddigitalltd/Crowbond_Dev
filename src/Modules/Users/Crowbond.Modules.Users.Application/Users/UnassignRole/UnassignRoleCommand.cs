using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Users.Application.Users.UnassignRole;

public sealed record UnassignRoleCommand(Guid UserId, string RoleName) : ICommand;
