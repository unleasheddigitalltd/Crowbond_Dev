﻿namespace Crowbond.Modules.Ticketing.Application.Customers.GetCustomer;

public sealed record CustomerResponse(Guid Id, string Email, string FirstName, string LastName);
