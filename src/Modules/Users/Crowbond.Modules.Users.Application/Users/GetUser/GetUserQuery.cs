using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Users.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;
