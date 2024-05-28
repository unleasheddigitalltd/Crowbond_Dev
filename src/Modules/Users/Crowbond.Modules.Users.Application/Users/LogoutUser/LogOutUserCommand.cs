using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Users.Application.Users.LogOutUser;
public sealed record LogOutUserCommand(string Username) : ICommand;
