using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(Guid UserId, CustomerRequest Customer) : ICommand<Guid>;
