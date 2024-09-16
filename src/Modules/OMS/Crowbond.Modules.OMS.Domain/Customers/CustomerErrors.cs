using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Customers;

public static class CustomerErrors
{
    public static Error NotFound(Guid customerId) =>
    Error.NotFound("Customers.NotFound", $"The customer with the identifier {customerId} was not found");

    public static Error ContactNotFound(Guid customerContactId) =>
    Error.NotFound("Customers.ContactNotFound", $"The customer contact with the identifier {customerContactId} was not found");
}
