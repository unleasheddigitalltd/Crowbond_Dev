using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductBlacklist;

public sealed record GetCustomerProductBlacklistQuery(Guid CustomerId, Guid CategoryId) : IQuery<IReadOnlyCollection<ProductResponse>;
