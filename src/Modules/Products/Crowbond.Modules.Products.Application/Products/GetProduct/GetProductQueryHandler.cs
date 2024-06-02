using System.Data.Common;
using Crowbond.Common.Application.Data;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.Products.Application.Products.GetProduct.Dtos;
using Crowbond.Modules.Products.Domain.Products;
using Dapper;

namespace Crowbond.Modules.Products.Application.Products.GetProduct;

internal sealed class GetProductQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetProductQuery, ProductResponse>
{
    public async Task<Result<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(ProductResponse.Id)},
                 sku AS {nameof(ProductResponse.Sku)},
                 name AS {nameof(ProductResponse.Name)},
                 parent_id AS {nameof(ProductResponse.Parent)},
                 filter_type_name AS {nameof(ProductResponse.FilterType)},
                 unit_of_measure_name AS {nameof(ProductResponse.UnitOfMeasure)},
                 category_id AS {nameof(ProductResponse.Category)},
                 inventory_type_name AS {nameof(ProductResponse.InventoryType)},
                 barcode AS {nameof(ProductResponse.Barcode)},
                 pack_size AS {nameof(ProductResponse.PackSize)},
                 handling_notes AS {nameof(ProductResponse.HandlingNotes)},
                 qi_check AS {nameof(ProductResponse.QiCheck)},
                 notes AS {nameof(ProductResponse.Notes)},
                 reorder_level AS {nameof(ProductResponse.ReorderLevel)},
                 height AS {nameof(ProductResponse.Height)},
                 width AS {nameof(ProductResponse.Width)},
                 length AS {nameof(ProductResponse.Length)},
                 weight_input AS {nameof(ProductResponse.WeightInput)},
                 active AS {nameof(ProductResponse.Active)}
             FROM products.products
             WHERE id = @ProductId
             """;

        ProductResponse? product = await connection.QuerySingleOrDefaultAsync<ProductResponse>(sql, request);

        if (product is null)
        {
            return Result.Failure<ProductResponse>(ProductErrors.NotFound(request.ProductId));
        }

        return product;
    }
}
