using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Suppliers;
public static class SupplierErrors
{
    public static Error NotFound(Guid supplierId) =>
    Error.NotFound("Suppliers.NotFound", $"The supplier with the identifier {supplierId} was not found");

    public static Error FilterTypeNotFound(string filterTypeName) =>
    Error.NotFound("Suppliers.NotFound", $"The filter type with the name {filterTypeName} was not found");

    public static readonly Error AlreadyHeld = Error.Problem(
        "Supplier.AlreadyHeld",
        "The supplier was already held");

    public static readonly Error AlreadyActivated = Error.Problem(
        "Supplier.AlreadyActivated",
        "The supplier was already activated");
}
