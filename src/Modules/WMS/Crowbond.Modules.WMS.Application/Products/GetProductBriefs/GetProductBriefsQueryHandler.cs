using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Products.GetProductBriefs;

internal sealed class GetProductBriefsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetProductBriefsQuery, IReadOnlyCollection<ProductResponse>>
{
    public async Task<Result<IReadOnlyCollection<ProductResponse>>> Handle(GetProductBriefsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(ProductResponse.Id)},
                 name AS {nameof(ProductResponse.Name)}
             FROM wms.products
             WHERE active = true
             ORDER BY name
             """;

        List<ProductResponse> products = (await connection.QueryAsync<ProductResponse>(sql, request)).AsList();

        return products;
    }
}
