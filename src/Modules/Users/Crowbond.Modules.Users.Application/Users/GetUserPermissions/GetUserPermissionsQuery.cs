using Crowbond.Common.Application.Authorization;
using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Users.Application.Users.GetUserPermissions;

public sealed record GetUserPermissionsQuery(string IdentityId) : IQuery<PermissionsResponse>;
