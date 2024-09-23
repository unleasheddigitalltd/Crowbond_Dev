using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProduct;

public sealed record GetCustomerProductQuery(Guid CustomerId, Guid ProductId) : IQuery<CustomerProductResponse>;
