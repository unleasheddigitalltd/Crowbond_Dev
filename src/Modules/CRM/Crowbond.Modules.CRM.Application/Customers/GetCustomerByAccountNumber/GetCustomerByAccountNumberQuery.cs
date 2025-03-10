using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomerByAccountNumber;

public sealed record GetCustomerByAccountNumberQuery(string AccountNumber) : IQuery<CustomerForOrderResponse>;
