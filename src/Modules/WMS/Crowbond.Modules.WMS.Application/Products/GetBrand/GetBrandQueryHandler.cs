using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Products;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Products.GetBrand;

internal sealed class GetBrandQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetBrandQuery, BrandResponse>
{
    public async Task<Result<BrandResponse>> Handle(GetBrandQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(BrandResponse.Id)},
                 name AS {nameof(BrandResponse.Name)}
             FROM wms.brands
             WHERE id = @BrandId
             """;

        BrandResponse? brand = await connection.QuerySingleOrDefaultAsync<BrandResponse>(sql, request);

        if (brand is null)
        {
            return Result.Failure<BrandResponse>(ProductErrors.BrandNotFound(request.BrandId));
        }

        return brand;
    }
}
