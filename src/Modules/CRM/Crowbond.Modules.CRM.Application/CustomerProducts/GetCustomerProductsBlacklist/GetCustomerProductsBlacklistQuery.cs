using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductsBlacklist;

public sealed record GetCustomerProductsBlacklistQuery(
    Guid CustomerId,
    string Search,
    string Sort,
    string Order,
    int Page,
    int Size) : IQuery<CustomerProductsResponse>;
