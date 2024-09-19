using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomerForOrder;

public sealed record GetCustomerForOrderQuery(Guid ContactId) : IQuery<CustomerForOrderResponse>;
