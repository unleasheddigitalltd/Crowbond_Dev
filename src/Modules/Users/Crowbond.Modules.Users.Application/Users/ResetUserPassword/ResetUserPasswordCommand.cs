using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.Users.Application.Users.ResetUserPassword;
public sealed record ResetUserPasswordCommand(string Email) : ICommand;
