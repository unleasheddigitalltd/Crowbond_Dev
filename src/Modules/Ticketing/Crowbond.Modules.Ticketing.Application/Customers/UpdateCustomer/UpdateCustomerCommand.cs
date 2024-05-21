using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Ticketing.Application.Customers.UpdateCustomer;

public sealed record UpdateCustomerCommand(Guid CustomerId, string FirstName, string LastName) : ICommand;
