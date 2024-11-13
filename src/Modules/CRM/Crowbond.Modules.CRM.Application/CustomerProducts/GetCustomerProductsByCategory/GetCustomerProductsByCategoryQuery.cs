using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductsByCategory;

public sealed record GetCustomerProductsByCategoryQuery(
    Guid CustomerId,
    Guid CategoryId, 
    string Search,
    string Sort,
    string Order,
    int Page,
    int Size) : IQuery<CustomerProductsResponse>;
