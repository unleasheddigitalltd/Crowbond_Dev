using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Customers.GetUserContactQuery;

public sealed record GetCustomerContactQuery(Guid CustomerContactId) : IQuery<CustomerContactResponse>;
