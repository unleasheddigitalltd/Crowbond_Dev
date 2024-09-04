using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.CompletePutAwayTask;

public sealed record CompletePutAwayTaskCommand(Guid UserId, Guid TaskHeaderId) : ICommand;
