using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.SupplierProducts;

public static class SupplierProductErrors
{
    public static Error NotFound(Guid supplierId, Guid prodictId) =>
    Error.NotFound("SupplierProducts.NotFound", $"The record for the supplier id {supplierId}, and the product id {prodictId} was not found");
}
