using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Users.ActivateUser;

public sealed record ActivateUserCommand(Guid UserId) : ICommand;
