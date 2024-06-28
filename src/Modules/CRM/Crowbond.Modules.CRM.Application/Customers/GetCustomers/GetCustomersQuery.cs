using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.CRM.Application.Customers.GetCustomers.Dto;

namespace Crowbond.Modules.CRM.Application.Customers.GetCustomers;

public sealed record GetCustomersQuery(string Search, string Sort, string Order, int Page, int Size) : IQuery<CustomersResponse>;
