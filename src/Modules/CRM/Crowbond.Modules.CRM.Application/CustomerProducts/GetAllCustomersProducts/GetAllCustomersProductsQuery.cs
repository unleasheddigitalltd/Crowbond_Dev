using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetAllCustomersProducts;

public sealed record GetAllCustomersProductsQuery(
    string Search,
    string Sort,
    string Order,
    int Page,
    int Size) : IQuery<AllCustomersProductsResponse>;
