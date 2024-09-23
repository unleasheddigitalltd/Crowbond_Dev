using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomerForOrderByContactId;

public sealed record GetCustomerForOrderByContactIdQuery(Guid ContactId) : IQuery<CustomerForOrderResponse>;
