using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Customers;
public static class CustomerErrors
{
    public static Error NotFound(Guid customerId) =>
    Error.NotFound("Customers.NotFound", $"The customer with the identifier {customerId} was not found");

    public static Error FilterTypeNotFound(string filterTypeName) =>
    Error.NotFound("Customers.NotFound", $"The filter type with the name {filterTypeName} was not found");

    public static readonly Error AlreadyHeld = Error.Problem(
        "Customer.AlreadyHeld",
        "The customer was already held");

    public static readonly Error AlreadyActivated = Error.Problem(
        "Customer.AlreadyActivated",
        "The customer was already activated");
}
