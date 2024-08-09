using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Suppliers;

public static class SupplierErrors
{
    public static Error NotFound(Guid supplierId) =>
    Error.NotFound("Suppliers.NotFound", $"The supplier with the identifier {supplierId} was not found");

}
