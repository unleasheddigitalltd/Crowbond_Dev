using Crowbond.Common.Domain;

namespace Crowbond.Modules.OMS.Domain.Customers;

public static class CustomerErrors
{
    public static Error NotFound(Guid customerId) =>
    Error.NotFound("Customers.NotFound",
        $"The customer with the identifier {customerId} was not found");

    public static Error ContactNotFound(Guid contactId) =>
    Error.NotFound("Customers.ContactNotFound",
        $"The customer with the contact identifier {contactId} was not found");

    public static Error OutletNotFound(Guid outletId) =>
    Error.NotFound("Customers.OutletNotFound",
        $"The outlet for this customer with the outlet identifier {outletId} was not found");

    public static readonly Error InvalidOutletForCustomer = Error.Problem(
    "CustomerOutlet.InvalidOutletForCustomer",
    "The specified outlet does not belong to the customer");
}
