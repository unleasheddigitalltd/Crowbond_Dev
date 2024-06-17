using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Products.GetProductDetails.Dtos;
using Crowbond.Modules.WMS.Domain.Products;
using Dapper;

namespace Crowbond.Modules.WMS.Application.Products.GetProductDetails;

internal sealed class GetProductDetailsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetProductDetailsQuery, ProductDetailsResponse>
{
    public async Task<Result<ProductDetailsResponse>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(ProductDetailsResponse.Id)},
                 sku AS {nameof(ProductDetailsResponse.Sku)},
                 name AS {nameof(ProductDetailsResponse.Name)},
                 parent_id AS {nameof(ProductDetailsResponse.Parent)},
                 filter_type_name AS {nameof(ProductDetailsResponse.FilterType)},
                 unit_of_measure_name AS {nameof(ProductDetailsResponse.UnitOfMeasure)},
                 category_id AS {nameof(ProductDetailsResponse.Category)},
                 inventory_type_name AS {nameof(ProductDetailsResponse.InventoryType)},
                 barcode AS {nameof(ProductDetailsResponse.Barcode)},
                 pack_size AS {nameof(ProductDetailsResponse.PackSize)},
                 handling_notes AS {nameof(ProductDetailsResponse.HandlingNotes)},
                 qi_check AS {nameof(ProductDetailsResponse.QiCheck)},
                 notes AS {nameof(ProductDetailsResponse.Notes)},
                 reorder_level AS {nameof(ProductDetailsResponse.ReorderLevel)},
                 height AS {nameof(ProductDetailsResponse.Height)},
                 width AS {nameof(ProductDetailsResponse.Width)},
                 length AS {nameof(ProductDetailsResponse.Length)},
                 weight_input AS {nameof(ProductDetailsResponse.WeightInput)},
                 active AS {nameof(ProductDetailsResponse.Active)}
             FROM wms.products
             WHERE id = @ProductId
             """;

        ProductDetailsResponse? product = await connection.QuerySingleOrDefaultAsync<ProductDetailsResponse>(sql, request);

        if (product is null)
        {
            return Result.Failure<ProductDetailsResponse>(ProductErrors.NotFound(request.ProductId));
        }

        return product;
    }
}
