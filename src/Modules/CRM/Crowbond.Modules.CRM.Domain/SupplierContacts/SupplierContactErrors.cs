using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.SupplierContacts;

public static class SupplierContactErrors
{
    public static Error NotFound(Guid supplierContactId) =>
    Error.NotFound("upplierContact.NotFound", $"The supplier contact with the identifier {supplierContactId} was not found");
}
