using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Products.GetInventoryTypes;

internal sealed class GetInventoryTypesQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetInventoryTypesQuery, IReadOnlyCollection<InventoryTypeResponse>>
{
    public async Task<Result<IReadOnlyCollection<InventoryTypeResponse>>> Handle(GetInventoryTypesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 name AS {nameof(InventoryTypeResponse.Name)}
             FROM wms.inventory_types
             """;

        List<InventoryTypeResponse> categories = (await connection.QueryAsync<InventoryTypeResponse>(sql, request)).AsList();

        return categories;
    }
}
