using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Domain.Products;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Products.GetProductGroup;

internal sealed class GetProductGroupQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetProductGroupQuery, ProductGroupResponse>
{
    public async Task<Result<ProductGroupResponse>> Handle(GetProductGroupQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(ProductGroupResponse.Id)},
                 name AS {nameof(ProductGroupResponse.Name)}
             FROM wms.product_groups
             WHERE id = @ProductGroupId
             """;

        ProductGroupResponse? productGroup = await connection.QuerySingleOrDefaultAsync<ProductGroupResponse>(sql, request);

        if (productGroup is null)
        {
            return Result.Failure<ProductGroupResponse>(ProductErrors.ProductGroupNotFound(request.ProductGroupId));
        }

        return productGroup;
    }
}
