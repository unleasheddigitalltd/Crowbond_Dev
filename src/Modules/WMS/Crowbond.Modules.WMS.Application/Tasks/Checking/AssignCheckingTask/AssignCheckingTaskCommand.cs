using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.AssignCheckingTask;

public sealed record AssignCheckingTaskCommand(Guid TaskHeaderId, Guid WarehouseOperatorId) : ICommand<Guid>;
