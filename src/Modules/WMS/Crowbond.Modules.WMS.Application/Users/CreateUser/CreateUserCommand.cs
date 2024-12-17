using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Users.CreateUser;

public sealed record CreateUserCommand(
    Guid UserId,
    string Username,
    string Email,
    string FirstName,
    string LastName,
    string Mobile) : ICommand;
