using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Users.Application.Users.CreateDriver;

public sealed record CreateDriverCommand(Guid UserId, string Email, string Username, string FirstName, string LastName) : ICommand;
