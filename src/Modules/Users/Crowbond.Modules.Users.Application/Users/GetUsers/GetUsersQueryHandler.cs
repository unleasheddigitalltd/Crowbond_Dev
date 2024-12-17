using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Pagination;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.Users.Application.Users.GetUsers;

internal sealed class GetUsersQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetUsersQuery, UsersResponse>
{
    public async Task<Result<UsersResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "id" => "u.username",
            "sku" => "u.email",
            "name" => "u.first_name",
            "categoryName" => "u.last_name",
            "isActive" => "u.is_active",
            _ => "u.last_name" // Default sorting
        };

        string sql = $@"
        WITH FilteredUsers AS (
            SELECT
                u.id AS {nameof(User.Id)},
                u.username AS {nameof(User.Username)},
                u.email AS {nameof(User.Email)},
                u.first_name AS {nameof(User.FirstName)},
                u.last_name AS {nameof(User.LastName)},
                u.mobile AS {nameof(User.Mobile)},
                u.is_active AS {nameof(User.IsActive)},
                STRING_AGG(ur.role_name, ',') AS {nameof(User.Roles)},
                ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
            FROM users.users u
            INNER JOIN users.user_roles ur ON u.id = ur.user_id
            WHERE
                u.username ILIKE '%' || @Search || '%'
                OR u.email ILIKE '%' || @Search || '%'
                OR u.first_name ILIKE '%' || @Search || '%'
                OR u.last_name ILIKE '%' || @Search || '%'
                OR u.mobile ILIKE '%' || @Search || '%'
            GROUP BY u.id, u.username, u.email, u.first_name, u.last_name, u.mobile, u.is_active
        )
        SELECT 
            u.{nameof(User.Id)},
            u.{nameof(User.Username)},
            u.{nameof(User.Email)},
            u.{nameof(User.FirstName)},
            u.{nameof(User.LastName)},
            u.{nameof(User.Mobile)},
            u.{nameof(User.IsActive)},
            u.{nameof(User.Roles)}
        FROM FilteredUsers u
        WHERE u.RowNum BETWEEN (@Page * @Size + 1) AND ((@Page + 1) * @Size)
        ORDER BY u.RowNum;

        SELECT COUNT(u.id)
        FROM users.users u
        INNER JOIN users.user_roles ur ON u.id = ur.user_id
        WHERE
            u.username ILIKE '%' || @Search || '%'
            OR u.email ILIKE '%' || @Search || '%'
            OR u.first_name ILIKE '%' || @Search || '%'
            OR u.last_name ILIKE '%' || @Search || '%'
            OR u.mobile ILIKE '%' || @Search || '%';
        ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var users = (await multi.ReadAsync<User>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = currentPage * pageSize;
        int endIndex = totalCount == 0 ? 0 : Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new UsersResponse(users, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}
