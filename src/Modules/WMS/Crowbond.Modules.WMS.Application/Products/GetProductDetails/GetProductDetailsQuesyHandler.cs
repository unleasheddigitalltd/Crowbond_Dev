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
                 id AS {nameof(ProductDetailsResponse.Id)},
                 sku AS {nameof(ProductDetailsResponse.Sku)},
                 name AS {nameof(ProductDetailsResponse.Name)},
                 parent_id AS {nameof(ProductDetailsResponse.Parent)},
                 filter_type_name AS {nameof(ProductDetailsResponse.FilterTypeName)},
                 unit_of_measure_name AS {nameof(ProductDetailsResponse.UnitOfMeasureName)},
                 category_id AS {nameof(ProductDetailsResponse.CategoryId)},
                 brand_id AS {nameof(ProductDetailsResponse.BrandId)},
                 product_group_id AS {nameof(ProductDetailsResponse.ProductGroupId)},
                 inventory_type_name AS {nameof(ProductDetailsResponse.InventoryTypeName)},
                 tax_rate_type AS {nameof(ProductDetailsResponse.TaxRateType)},
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
                 is_active AS {nameof(ProductDetailsResponse.IsActive)}
             FROM wms.products
             WHERE id = @ProductId
             """;

        ProductDetailsResponse? product = await connection.QuerySingleOrDefaultAsync<ProductDetailsResponse>(sql, request);

        if (product is null)
        {
            return Result.Failure<ProductDetailsResponse>(ProductErrors.NotFound(request.ProductId));
        }
        
        product = product with { Images = new[] { $"https://crowbond-images.s3.eu-west-1.amazonaws.com/{product.Sku}.png" } };

        return product;
    }
}
