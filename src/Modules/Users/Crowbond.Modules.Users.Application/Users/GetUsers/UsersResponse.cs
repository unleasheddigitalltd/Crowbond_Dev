using Crowbond.Common.Application.Pagination;

namespace Crowbond.Modules.Users.Application.Users.GetUsers;

public sealed class UsersResponse : PaginatedResponse<User>
{
    public UsersResponse(IReadOnlyCollection<User> users, IPagination pagination)
        : base(users, pagination)
    { }
}

public sealed record User (Guid Id, string Username, string Email, string FirstName, string LastName, string Mobile, bool IsActive, string Roles);
