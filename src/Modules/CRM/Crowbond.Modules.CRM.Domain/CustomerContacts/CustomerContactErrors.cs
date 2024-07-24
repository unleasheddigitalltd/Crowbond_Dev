using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.CustomerContacts;

public static class CustomerContactErrors
{    
    public static Error NotFound(Guid customerContactId) =>
    Error.NotFound("CustomerContact.NotFound", $"The customer contact with the identifier {customerContactId} was not found");
}
