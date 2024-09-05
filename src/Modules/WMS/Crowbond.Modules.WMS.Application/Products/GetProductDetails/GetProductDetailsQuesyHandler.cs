using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
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
                 p.id AS {nameof(ProductDetailsResponse.Id)},
                 p.sku AS {nameof(ProductDetailsResponse.Sku)},
                 p.name AS {nameof(ProductDetailsResponse.Name)},
                 p.parent_id AS {nameof(ProductDetailsResponse.Parent)},
                 p.filter_type_name AS {nameof(ProductDetailsResponse.FilterTypeName)},
                 p.unit_of_measure_name AS {nameof(ProductDetailsResponse.UnitOfMeasureName)},
                 c.id AS {nameof(ProductDetailsResponse.CategoryId)},
                 c.name AS {nameof(ProductDetailsResponse.CategoryName)},
                 p.inventory_type_name AS {nameof(ProductDetailsResponse.InventoryTypeName)},
                 p.tax_rate_type AS {nameof(ProductDetailsResponse.TaxRateType)},
                 p.barcode AS {nameof(ProductDetailsResponse.Barcode)},
                 p.pack_size AS {nameof(ProductDetailsResponse.PackSize)},
                 p.handling_notes AS {nameof(ProductDetailsResponse.HandlingNotes)},
                 p.qi_check AS {nameof(ProductDetailsResponse.QiCheck)},
                 p.notes AS {nameof(ProductDetailsResponse.Notes)},
                 p.reorder_level AS {nameof(ProductDetailsResponse.ReorderLevel)},
                 p.height AS {nameof(ProductDetailsResponse.Height)},
                 p.width AS {nameof(ProductDetailsResponse.Width)},
                 p.length AS {nameof(ProductDetailsResponse.Length)},
                 p.weight_input AS {nameof(ProductDetailsResponse.WeightInput)},
                 p.is_active AS {nameof(ProductDetailsResponse.IsActive)}
             FROM wms.products p
             INNER JOIN wms.categories c ON p.category_id = c.id
             WHERE p.id = @ProductId
             """;

        ProductDetailsResponse? product = await connection.QuerySingleOrDefaultAsync<ProductDetailsResponse>(sql, request);

        if (product is null)
        {
            return Result.Failure<ProductDetailsResponse>(ProductErrors.NotFound(request.ProductId));
        }

        return product;
    }
}
