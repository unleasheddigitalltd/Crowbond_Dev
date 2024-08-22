using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.SupplierContacts;

public static class SupplierContactErrors
{
    public static Error NotFound(Guid supplierContactId) =>
    Error.NotFound("SupplierContact.NotFound", $"The supplier contact with the identifier {supplierContactId} was not found");


    public static readonly Error AlreadyActivated = Error.Problem(
    "SupplierContact.AlreadyActivated",
    "The contact was already activated");

    public static readonly Error AlreadyDeactivated = Error.Problem(
    "SupplierContact.AlreadyDeactivated",
    "The contact was already deactivated");

    public static readonly Error IsNotActive = Error.Problem(
    "SupplierContact.IsNotActive",
    "The contact was not active");

    public static readonly Error IsPrimary = Error.Problem(
    "SupplierContact.IsPrimary",
    "The contact was primary");
}
