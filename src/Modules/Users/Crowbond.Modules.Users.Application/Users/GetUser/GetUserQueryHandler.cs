using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Domain.Users;
using Dapper;

namespace Crowbond.Modules.Users.Application.Users.GetUser;

internal sealed class GetUserQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetUserQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 u.id AS {nameof(UserResponse.Id)},
                 u.username AS {nameof(UserResponse.Username)},
                 u.email AS {nameof(UserResponse.Email)},
                 u.first_name AS {nameof(UserResponse.FirstName)},
                 u.last_name AS {nameof(UserResponse.LastName)},
                 STRING_AGG(ur.role_name, ',') AS {nameof(UserResponse.Roles)}
             FROM users.users u
             INNER JOIN users.user_roles ur ON u.id = ur.user_id             
             WHERE u.id = @UserId
             GROUP BY u.id, u.username, u.email, u.first_name, u.last_name;
             """;

        UserResponse? user = await connection.QuerySingleOrDefaultAsync<UserResponse>(sql, request);

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(request.UserId));
        }

        return user;
    }
}
