using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.GetCustomerContact;

public sealed record GetCustomerContactQuery(Guid CustomerContactId) : IQuery<CustomerContactResponse>;
