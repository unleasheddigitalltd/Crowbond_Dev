using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProducts;

public sealed record GetCustomerProductsQuery(
    Guid CustomerId,
    string Search,
    string Sort,
    string Order,
    int Page,
    int Size) : IQuery<CustomerProductsResponse>;
