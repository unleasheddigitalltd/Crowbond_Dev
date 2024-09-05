using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Products.GetFilterTypes;

internal sealed class GetFilterTypesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetFilterTypesQuery, IReadOnlyCollection<FilterTypeResponse>>
{
    public async Task<Result<IReadOnlyCollection<FilterTypeResponse>>> Handle(GetFilterTypesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 name AS {nameof(FilterTypeResponse.Name)}
             FROM wms.filter_types
             """;

        List<FilterTypeResponse> filterTypes = (await connection.QueryAsync<FilterTypeResponse>(sql, request)).AsList();

        return filterTypes;
    }
}
