using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.StartCheckingTask;

public sealed record StartCheckingTaskCommand(Guid UserId, Guid TaskHeaderId) : ICommand;
