using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.OMS.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(
    Guid UserId,
    string Username,
    string Email,
    string FirstName,
    string LastName,
    string Mobile) : ICommand;
