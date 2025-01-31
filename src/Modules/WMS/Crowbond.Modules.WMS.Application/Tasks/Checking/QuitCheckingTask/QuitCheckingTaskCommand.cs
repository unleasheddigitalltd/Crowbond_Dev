using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.QuitCheckingTask;

public sealed record QuitCheckingTaskCommand(Guid UserId, Guid TaskHeaderId) : ICommand;
