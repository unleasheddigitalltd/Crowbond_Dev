using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.SupplierProducts.GetSupplierProducts;
using Crowbond.Modules.CRM.PublicApi;
using MediatR;
using SupplierProductResponse = Crowbond.Modules.CRM.PublicApi.SupplierProductResponse;

namespace Crowbond.Modules.CRM.Infrastructure.PublicApi;

internal sealed class SupplierProductsApi(ISender sender) : ISupplierProductApi
{
    public async Task<SupplierProductResponse> GetAsync(Guid supplierId, Guid productId, CancellationToken cancellationToken = default)
    {
        Result<Application.SupplierProducts.GetSupplierProducts.SupplierProductResponse> result = 
            await sender.Send(new GetSupplierProductQuery(supplierId, productId), cancellationToken);

        if (result.IsFailure)
        {
            return null;
        }

        return new SupplierProductResponse(
                result.Value.Id,
                result.Value.SupplierId,
                result.Value.ProductId,
                result.Value.ProductName,
                result.Value.ProductSku,
                result.Value.UnitOfMeasureName,
                result.Value.CategoryId,
                result.Value.UnitPrice,
                result.Value.TaxRateType,
                result.Value.IsDefault,
                result.Value.Comments);
    }
}
