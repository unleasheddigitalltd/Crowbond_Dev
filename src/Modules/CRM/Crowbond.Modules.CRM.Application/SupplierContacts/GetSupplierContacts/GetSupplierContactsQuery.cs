using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.GetSupplierContacts;

public sealed record GetSupplierContactsQuery(Guid SupplierId) : IQuery<IReadOnlyCollection<SupplierContactResponse>>;
