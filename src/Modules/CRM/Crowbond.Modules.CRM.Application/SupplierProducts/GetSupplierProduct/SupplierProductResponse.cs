namespace Crowbond.Modules.CRM.Application.SupplierProducts.GetSupplierProduct;

public sealed record SupplierProductResponse
{
    public Guid Id { get; }
    public Guid SupplierId { get; }
    public Guid ProductId { get; }
    public string ProductName { get; }
    public string ProductSku { get; }
    public string UnitOfMeasureName { get; }
    public Guid CategoryId { get; }
    public decimal UnitPrice { get; }
    public int TaxRateType { get; }
    public bool IsDefault { get; }
    public string? Comments { get; }
}
