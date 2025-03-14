using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Users;

namespace Crowbond.Modules.Users.Application.Users.GetUserByIdentityId;

public sealed record GetUserByIdentityIdQuery(string IdentityId) : IQuery<UserResponse>;