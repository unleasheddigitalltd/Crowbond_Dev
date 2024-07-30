using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomerContacts;

public sealed record GetCustomerContactsQuery(Guid CustomerId) : IQuery<IReadOnlyCollection<CustomerContactResponse>>;
