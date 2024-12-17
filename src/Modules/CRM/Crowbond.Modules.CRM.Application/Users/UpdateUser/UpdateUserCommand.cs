using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(
    Guid UserId,
    string Username,
    string Email,
    string FirstName,
    string LastName,
    string Mobile) : ICommand;
