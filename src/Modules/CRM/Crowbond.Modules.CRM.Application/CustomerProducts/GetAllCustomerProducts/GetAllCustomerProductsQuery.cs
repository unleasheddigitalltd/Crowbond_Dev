using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetAllCustomerProducts;

public sealed record GetAllCustomerProductsQuery(
    string Search,
    string Sort,
    string Order,
    int Page,
    int Size) : IQuery<CustomerProductsResponse>;
