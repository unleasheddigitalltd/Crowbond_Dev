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
    decimal UnitPrice,
    int TaxRateType,
    bool IsDefault,
    string? Comments);
