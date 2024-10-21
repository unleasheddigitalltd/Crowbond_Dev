using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;
using System.Data.Common;

namespace Crowbond.Modules.WMS.Application.Products.GetProductGroups;

internal sealed class GetProductGroupsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetProductGroupsQuery, IReadOnlyCollection<ProductGroupResponse>>
{
    public async Task<Result<IReadOnlyCollection<ProductGroupResponse>>> Handle(GetProductGroupsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(ProductGroupResponse.Id)},
                 name AS {nameof(ProductGroupResponse.Name)}
             FROM wms.product_groups
             """;

        List<ProductGroupResponse> productGroups = (await connection.QueryAsync<ProductGroupResponse>(sql, request)).AsList();

        return productGroups;
    }
}
