using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomerDetails;

public sealed record GetCustomerDetailsQuery(Guid CustomerId) : IQuery<CustomerDetailsResponse>;

