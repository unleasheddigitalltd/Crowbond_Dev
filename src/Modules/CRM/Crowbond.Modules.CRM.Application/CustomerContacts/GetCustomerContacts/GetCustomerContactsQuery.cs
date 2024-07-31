using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerContacts.GetCustomerContacts;

public sealed record GetCustomerContactsQuery(Guid CustomerId) : IQuery<IReadOnlyCollection<CustomerContactResponse>>;
