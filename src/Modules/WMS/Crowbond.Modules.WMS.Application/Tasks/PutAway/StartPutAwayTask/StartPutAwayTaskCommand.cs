using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.StartPutAwayTask;

public sealed record StartPutAwayTaskCommand(Guid UserId, Guid TaskHeaderId) : ICommand;
