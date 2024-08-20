using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Suppliers;
public static class SupplierErrors
{
    public static Error NotFound(Guid supplierId) =>
    Error.NotFound("Suppliers.NotFound", $"The supplier with the identifier {supplierId} was not found");

    public static Error FilterTypeNotFound(string filterTypeName) =>
    Error.NotFound("Suppliers.NotFound", $"The filter type with the name {filterTypeName} was not found");

    public static readonly Error AlreadyDeactivated = Error.Problem(
        "Supplier.AlreadyDeactivated",
        "The supplier was already deactivated");

    public static readonly Error AlreadyActivated = Error.Problem(
        "Supplier.AlreadyActivated",
        "The supplier was already activated");


    public static Error SequenceNotFound() =>
    Error.NotFound("Sequence.NotFound", $"The sequence for the supplier type was not found");
}
