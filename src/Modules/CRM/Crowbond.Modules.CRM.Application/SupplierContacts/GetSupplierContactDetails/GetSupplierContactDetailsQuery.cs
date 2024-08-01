using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.SupplierContacts.GetSupplierContactDetails;

public sealed record GetSupplierContactDetailsQuery(Guid SupplierContactId) : IQuery<SupplierContactDetailsResponse>;
