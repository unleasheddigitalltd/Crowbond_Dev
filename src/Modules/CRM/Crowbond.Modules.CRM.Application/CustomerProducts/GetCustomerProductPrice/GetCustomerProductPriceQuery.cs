using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductPrice;

public sealed record GetCustomerProductPriceQuery(Guid CustomerId, Guid ProductId) : IQuery<CustomerProductPriceResponse>;
