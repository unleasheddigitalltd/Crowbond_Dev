namespace Crowbond.Modules.CRM.PublicApi;

public interface ICustomerProductApi
{
    Task<CustomerProductResponse> GetAsync(Guid customerId, Guid productId, CancellationToken cancellationToken = default);

    Task<CustomerProductResponse> GetBySkuAsync(Guid customerId, string productSku,
        CancellationToken cancellationToken = default);
}

public sealed record CustomerProductResponse(
    Guid Id,
    Guid CustomerId,
    Guid ProductId,
    string ProductName,
    string ProductSku,
    string UnitOfMeasureName,
    Guid CategoryId,
    string CategoryName,
    Guid BrandId,
    string BrandName,
    Guid ProductGroupId,
    string ProductGroupName,
    decimal UnitPrice,
    int TaxRateType,
    bool IsBlacklisted);
