using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.PausePutAwayTask;

public sealed record PausePutAwayTaskCommand(Guid UserId, Guid TaskHeaderId) : ICommand;
