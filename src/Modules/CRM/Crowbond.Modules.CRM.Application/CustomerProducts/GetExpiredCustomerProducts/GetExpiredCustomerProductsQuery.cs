using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetExpiredCustomerProducts;

public sealed record GetExpiredCustomerProductsQuery(
    DateOnly ExpiryDate,
    string Search,
    string Sort,
    string Order,
    int Page,
    int Size) : IQuery<CustomerProductsResponse>;
