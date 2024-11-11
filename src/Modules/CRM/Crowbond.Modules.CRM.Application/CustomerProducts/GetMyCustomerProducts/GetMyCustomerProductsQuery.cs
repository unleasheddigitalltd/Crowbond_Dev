using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetMyCustomerProducts;
public sealed record GetMyCustomerProductsQuery(Guid CustomerContactId, string Search, string Sort, string Order, int Page, int Size)
    : IQuery<CustomerProductsResponse>;
