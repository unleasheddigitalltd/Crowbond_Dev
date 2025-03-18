using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductPrice;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProductBySku;

public sealed record GetCustomerProductBySkuQuery(Guid CustomerId, string ProductSku) : IQuery<CustomerProductPriceResponse>;
