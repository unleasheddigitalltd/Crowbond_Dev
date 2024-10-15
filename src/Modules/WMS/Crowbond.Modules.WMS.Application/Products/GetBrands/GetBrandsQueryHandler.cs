using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Dapper;
using System.Data.Common;

namespace Crowbond.Modules.WMS.Application.Products.GetBrands;

internal sealed class GetBrandsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetBrandsQuery, IReadOnlyCollection<BrandResponse>>
{
    public async Task<Result<IReadOnlyCollection<BrandResponse>>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(BrandResponse.Id)},
                 name AS {nameof(BrandResponse.Name)}
             FROM wms.brands
             """;

        List<BrandResponse> brands = (await connection.QueryAsync<BrandResponse>(sql, request)).AsList();

        return brands;
    }
}
