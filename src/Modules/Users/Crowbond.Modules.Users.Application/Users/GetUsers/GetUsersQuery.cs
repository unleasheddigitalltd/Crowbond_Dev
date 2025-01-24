using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Users.Application.Users.GetUsers;

public sealed record GetUsersQuery(string Search, string Sort, string Order, int Page, int Size) : IQuery<UsersResponse>;
