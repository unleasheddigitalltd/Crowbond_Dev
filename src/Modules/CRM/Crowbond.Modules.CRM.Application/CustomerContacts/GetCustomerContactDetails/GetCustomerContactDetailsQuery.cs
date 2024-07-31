using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.GetCustomerContactDetails;

public sealed record GetCustomerContactDetailsQuery(Guid CustomerContactId) : IQuery<CustomerContactResponse>;
