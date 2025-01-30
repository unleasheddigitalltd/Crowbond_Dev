using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.UpdateCheckingTask;

public sealed record UpdateCheckingTaskCommand(
    Guid UserId,
    Guid TaskHeaderId,
    List<CheckingDispatchLineRequest> CheckingDispatchLines) 
    : ICommand;
