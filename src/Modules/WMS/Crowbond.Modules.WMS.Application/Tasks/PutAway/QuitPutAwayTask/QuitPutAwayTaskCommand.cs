using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.QuitPutAwayTask;

public sealed record QuitPutAwayTaskCommand(Guid UserId, Guid TaskHeaderId) : ICommand;
