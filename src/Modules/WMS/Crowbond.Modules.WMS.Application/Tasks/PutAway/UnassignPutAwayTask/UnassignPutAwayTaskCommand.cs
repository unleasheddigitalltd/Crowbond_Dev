using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.UnassignPutAwayTask;

public sealed record UnassignPutAwayTaskCommand(Guid UserId, Guid TaskHeaderId) : ICommand;
