using Crowbond.Common.Domain;

namespace Crowbond.Modules.CRM.Domain.SupplierContacts;

public sealed class SupplierContactActivatedDomainEvent(Guid supplierContactId) : DomainEvent
{
    public Guid SupplierContactId { get; init; } = supplierContactId;
}
