using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.SupplierProducts;

public static class SupplierProductErrors
{
    public static Error NotFound(Guid supplierId, Guid productId) =>
    Error.NotFound("SupplierProsuct.NotFound", $"The supplier product for supplier with the identifier {supplierId} and product with the identifier {productId} was not found");
}
