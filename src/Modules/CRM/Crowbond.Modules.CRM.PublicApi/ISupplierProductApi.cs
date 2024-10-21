namespace Crowbond.Modules.CRM.PublicApi;

public interface ISupplierProductApi
{
    Task<SupplierProductResponse> GetAsync(Guid supplierId, Guid productId, CancellationToken cancellationToken = default);
}

public sealed record SupplierProductResponse(
    Guid Id,
    Guid SupplierId,
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
    bool IsDefault,
    string? Comments);
