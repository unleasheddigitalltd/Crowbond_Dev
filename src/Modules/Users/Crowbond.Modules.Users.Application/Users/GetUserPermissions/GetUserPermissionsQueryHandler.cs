using System.Data.Common;
using Crowbond.Common.Application.Authorization;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Users.Domain.Users;
using Dapper;

namespace Crowbond.Modules.Users.Application.Users.GetUserPermissions;

internal sealed class GetUserPermissionsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetUserPermissionsQuery, PermissionsResponse>
{
    public async Task<Result<PermissionsResponse>> Handle(
        GetUserPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT DISTINCT
                 u.id AS {nameof(UserPermission.UserId)},
                 rp.permission_code AS {nameof(UserPermission.Permission)}
             FROM users.users u
             JOIN users.user_roles ur ON ur.user_id = u.id
             JOIN users.role_permissions rp ON rp.role_name = ur.role_name
             WHERE u.identity_id = @IdentityId
             """;

        Console.WriteLine($"Querying permissions for identity_id: {request.IdentityId}");
        var permissions = (await connection.QueryAsync<UserPermission>(sql, request)).AsList();
        Console.WriteLine($"Found {permissions.Count} permissions:");
        foreach (var permission in permissions)
        {
            Console.WriteLine($"- UserId: {permission.UserId}, Permission: {permission.Permission}");
        }

        if (!permissions.Any())
        {
            return Result.Failure<PermissionsResponse>(UserErrors.NotFound(request.IdentityId));
        }

        return new PermissionsResponse(permissions[0].UserId, permissions.Select(p => p.Permission).ToHashSet());
    }

    internal sealed class UserPermission
    {
        internal Guid UserId { get; init; }

        internal string Permission { get; init; }
    }
}
