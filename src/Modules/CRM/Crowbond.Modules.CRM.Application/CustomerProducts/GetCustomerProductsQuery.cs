using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts;

public sealed record GetCustomerProductsQuery(Guid CustomerId) : IQuery<IReadOnlyCollection<CustomerProductResponse>>;
