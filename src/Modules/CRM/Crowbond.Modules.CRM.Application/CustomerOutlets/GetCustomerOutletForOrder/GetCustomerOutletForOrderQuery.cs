using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutletForOrder;

public sealed record GetCustomerOutletForOrderQuery(Guid CustomerOutletId) : IQuery<CustomerOutletForOrderResponse>;
