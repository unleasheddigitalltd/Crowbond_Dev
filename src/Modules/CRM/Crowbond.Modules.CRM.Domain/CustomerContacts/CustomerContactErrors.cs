using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerContacts;

public static class CustomerContactErrors
{    
    public static Error NotFound(Guid customerContactId) =>
    Error.NotFound("CustomerContact.NotFound", $"The customer contact with the identifier {customerContactId} was not found");

    public static readonly Error AlreadyActivated = Error.Problem(
    "CustomerContact.AlreadyActivated",
    "The contact was already activated");

    public static readonly Error AlreadyDeactivated = Error.Problem(
    "CustomerContact.AlreadyDeactivated",
    "The contact was already deactivated");
}
