using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Customers;
public static class CustomerErrors
{
    public static Error NotFound(Guid customerId) =>
    Error.NotFound("Customers.NotFound", $"The customer with the identifier {customerId} was not found");

    public static readonly Error AlreadyDeactivated = Error.Problem(
        "Customer.AlreadyDeactivate",
        "The customer was already deactivated");

    public static readonly Error AlreadyActivated = Error.Problem(
        "Customer.AlreadyActivated",
        "The customer was already activated");

    public static Error SequenceNotFound() =>
    Error.NotFound("Customer.SequenceNotFound", $"The sequence for the customer type was not found");
}
