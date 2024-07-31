using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerOutlets.GetCustomerOutlet;
public sealed record GetCustomerOutletQuery(Guid CustomerOutletId) : IQuery<CustomerOutletResponse>;
