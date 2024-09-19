namespace Crowbond.Modules.CRM.PublicApi;

public interface ICustomerProductApi
{
    Task<CustomerProductResponse> GetAsync(Guid customerId, Guid productId, CancellationToken cancellationToken = default);
}

public sealed record CustomerProductResponse(
    Guid Id,
    Guid customerId,
    Guid ProductId,
    string ProductName,
    string ProductSku,
    string UnitOfMeasureName,
    Guid CategoryId,
    string CategoryName,
    decimal UnitPrice,
    bool IsFixedPrice,
    int TaxRateType);
