using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.UnpausePutAwayTask;

public sealed record UnpausePutAwayTaskCommand(Guid UserId, Guid TaskHeaderId) : ICommand;
