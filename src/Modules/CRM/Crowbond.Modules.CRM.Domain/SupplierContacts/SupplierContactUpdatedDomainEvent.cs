using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.SupplierContacts;
public sealed class SupplierContactUpdatedDomainEvent(Guid supplierContactId, string firstName, string lastName) : DomainEvent
{
    public Guid SupplierContactId { get; init; } = supplierContactId;
    public string FirstName { get; init; } = firstName;
    public string LastName { get; init; } = lastName;
}
