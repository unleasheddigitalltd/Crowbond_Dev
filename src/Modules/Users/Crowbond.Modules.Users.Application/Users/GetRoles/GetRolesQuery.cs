using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Users.Application.Users.GetRoles;

public sealed record GetRolesQuery() : IQuery<IReadOnlyCollection<RoleResponse>>;
