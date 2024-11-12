﻿using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.CustomerProducts.GetCustomerProducts;

public sealed record GetCustomerProductsQuery(Guid CustomerId, Guid CategoryId) : IQuery<IReadOnlyCollection<ProductResponse>>;
