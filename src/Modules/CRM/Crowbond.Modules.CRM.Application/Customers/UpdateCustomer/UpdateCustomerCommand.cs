﻿using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Customers.UpdateCustomer;

public sealed record UpdateCustomerCommand(Guid CustomerId, Guid UserName, CustomerRequest Customer) : ICommand;

