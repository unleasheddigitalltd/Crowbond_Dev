using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Users.Application.Users.CreateCustomer;

public sealed record CreateCustomerCommand(Guid UserId,string Email, string Username, string FirstName, string LastName) : ICommand;
