using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutletDetails;

public sealed record GetCustomerOutletDetailsQuery(Guid CustomerOutletId) : IQuery<CustomerOutletDetailsResponse>;
