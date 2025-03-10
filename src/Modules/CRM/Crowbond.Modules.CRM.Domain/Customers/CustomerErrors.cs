using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.Customers;
public static class CustomerErrors
{
    public static Error NotFound() => 
    Error.NotFound("Customers.NotFound", "The customer was not found");
    
    public static Error NotFound(Guid customerId) =>
    Error.NotFound("Customers.NotFound", $"The customer with the identifier {customerId} was not found");

    public static readonly Error AlreadyDeactivated = Error.Problem(
        "Customer.AlreadyDeactivate",
        "The customer was already deactivated");

    public static readonly Error AlreadyActivated = Error.Problem(
        "Customer.AlreadyActivated",
        "The customer was already activated");

    public static readonly Error CustomPaymentTermsParametersHasNoValue = Error.Problem(
        "Customer.CustomPaymentTermsHasNoValue",
        "The custom payment terms has no value for due days invoice or due date calculation basis.");

    public static Error SequenceNotFound() =>
    Error.NotFound("Customer.SequenceNotFound", $"The sequence for the customer type was not found");
}
