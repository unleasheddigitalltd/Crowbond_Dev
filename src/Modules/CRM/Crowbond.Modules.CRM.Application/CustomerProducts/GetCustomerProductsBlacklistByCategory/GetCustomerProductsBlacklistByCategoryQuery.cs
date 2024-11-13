using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductsBlacklistByCategory;

public sealed record GetCustomerProductsBlacklistByCategoryQuery(
    Guid CustomerId,
    Guid CategoryId,
    string Search,
    string Sort,
    string Order,
    int Page,
    int Size) : IQuery<CustomerProductsResponse>;
