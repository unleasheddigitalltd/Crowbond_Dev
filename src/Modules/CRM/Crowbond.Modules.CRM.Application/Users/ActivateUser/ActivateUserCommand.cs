using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.CRM.Application.Users.ActivateUser;

public sealed record ActivateUserCommand(Guid UserId): ICommand;
