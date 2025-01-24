using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Application.Pagination;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.OMS.Application.Routes.GetRoutes;

internal sealed class GetRoutesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetRoutesQuery, RoutesResponse>
{
    public async Task<Result<RoutesResponse>> Handle(GetRoutesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string sortOrder = request.Order.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "ASC" : "DESC";
        string orderByClause = request.Sort switch
        {
            "name" => "name",
            "position" => "position",
            _ => "position" // Default sorting
        };

        string sql = $@"WITH FilteredRoutes AS (
                SELECT 
                    id AS {nameof(Route.Id)},
                    name AS {nameof(Route.Name)},
                    position AS {nameof(Route.Position)},
                    cut_off_time AS {nameof(Route.CutOffTime)},
                    days_of_week AS {nameof(Route.DaysOfWeek)},
                    ROW_NUMBER() OVER (ORDER BY {orderByClause} {sortOrder}) AS RowNum
                FROM oms.routes
                WHERE
                    name ILIKE '%' || @Search || '%'
            )
            SELECT 
                r.{nameof(Route.Id)},
                r.{nameof(Route.Name)},
                r.{nameof(Route.Position)},
                r.{nameof(Route.CutOffTime)},
                r.{nameof(Route.DaysOfWeek)}
            FROM FilteredRoutes r
            WHERE r.RowNum BETWEEN ((@Page) * @Size) + 1 AND (@Page + 1) * @Size
            ORDER BY r.RowNum;

            SELECT COUNT(*)
                FROM oms.routes
                WHERE
                    name ILIKE '%' || @Search || '%'
        ";

        SqlMapper.GridReader multi = await connection.QueryMultipleAsync(sql, request);

        var receipts = (await multi.ReadAsync<Route>()).ToList();
        int totalCount = await multi.ReadSingleAsync<int>();

        int totalPages = (int)Math.Ceiling(totalCount / (double)request.Size);
        int currentPage = request.Page;
        int pageSize = request.Size;
        int startIndex = (currentPage - 1) * pageSize;
        int endIndex = totalCount == 0 ? 0 : Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new RoutesResponse(receipts, new Pagination(totalCount, pageSize, currentPage, totalPages, startIndex, endIndex));
    }
}
