using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.GetSupplierContact;

public sealed record GetSupplierContactQuery(Guid SupplierContactId) : IQuery<SupplierContactResponse>;
