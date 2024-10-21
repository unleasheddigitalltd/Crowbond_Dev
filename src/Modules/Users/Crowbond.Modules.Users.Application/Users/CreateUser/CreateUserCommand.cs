using Crowbond.Common.Application.Messaging;
using Crowbond.Modules.Users.Domain.Users;

namespace Crowbond.Modules.Users.Application.Users.CreateUser;

public sealed record CreateUserCommand(
    Guid UserId,
    string Email,
    string Username,
    string FirstName,
    string LastName,
    string Mobile,
    Role Role) : ICommand;
