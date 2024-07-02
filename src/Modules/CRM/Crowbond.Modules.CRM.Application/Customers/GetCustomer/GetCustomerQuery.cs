using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomer;

public sealed record GetCustomerQuery(Guid CustomerId) : IQuery<CustomerResponse>;

