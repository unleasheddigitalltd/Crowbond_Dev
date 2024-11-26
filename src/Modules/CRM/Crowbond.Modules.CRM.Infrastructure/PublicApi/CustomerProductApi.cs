using Crowbond.Common.Domain;
using Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductPrice;
using Crowbond.Modules.CRM.PublicApi;
using MediatR;

namespace Crowbond.Modules.CRM.Infrastructure.PublicApi;

internal sealed class CustomerProductApi(ISender sender) : ICustomerProductApi
{
    public async Task<CustomerProductResponse> GetAsync(Guid customerId, Guid productId, CancellationToken cancellationToken = default)
    {
        Result<CustomerProductPriceResponse> result =
            await sender.Send(new GetCustomerProductPriceQuery(customerId, productId), cancellationToken);

        if (result.IsFailure)
        {
            return null;
        }

        return new CustomerProductResponse(
                result.Value.Id,
                result.Value.CustomerId,
                result.Value.ProductId,
                result.Value.ProductName,
                result.Value.ProductSku,
                result.Value.UnitOfMeasureName,
                result.Value.CategoryId,
                result.Value.CategoryName,
                result.Value.BrandId,
                result.Value.BrandName,
                result.Value.ProductGroupId,
                result.Value.ProductGroupName,
                result.Value.UnitPrice,
                result.Value.TaxRateType,
                result.Value.IsBlacklisted);
    }
}
