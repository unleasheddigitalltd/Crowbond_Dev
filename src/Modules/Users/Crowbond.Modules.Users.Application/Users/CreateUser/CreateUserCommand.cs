using Crowbond.Common.Application.EventBus;
using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Users.Application.Users.CreateUser;

public sealed record CreateUserCommand(Guid UserId,string Email, string Username, string FirstName, string LastName) : ICommand;
