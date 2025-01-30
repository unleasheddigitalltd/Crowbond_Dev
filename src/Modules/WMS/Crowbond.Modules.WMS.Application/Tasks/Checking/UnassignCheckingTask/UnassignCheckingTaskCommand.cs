using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.UnassignCheckingTask;

public sealed record UnassignCheckingTaskCommand(Guid TaskHeaderId) : ICommand;
