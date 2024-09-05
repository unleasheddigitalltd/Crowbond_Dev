namespace Crowbond.Modules.CRM.Application.SupplierProducts.UpdateSupplierProducts;

public sealed record SupplierProductRequest(Guid ProductId, decimal UnitPrice, bool IsDefault, string? Comments);
