using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.Users.Application.Users.GetRoles;

internal sealed class GetRolesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRolesQuery, IReadOnlyCollection<RoleResponse>>
{
    public async Task<Result<IReadOnlyCollection<RoleResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 name AS {nameof(RoleResponse.Name)}
             FROM users.roles
             """;

        List<RoleResponse> roles = (await connection.QueryAsync<RoleResponse>(sql, request)).AsList();

        return roles;
    }
}
