﻿using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomerOutlets;
public sealed record GetCustomerOutletsQuery(Guid CustomerId) : IQuery<IReadOnlyCollection<CustomerOutletResponse>>;
