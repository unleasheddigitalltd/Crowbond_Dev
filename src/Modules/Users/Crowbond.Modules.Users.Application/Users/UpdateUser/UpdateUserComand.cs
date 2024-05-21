using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Users.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(Guid UserId, string FirstName, string LastName) : ICommand;
