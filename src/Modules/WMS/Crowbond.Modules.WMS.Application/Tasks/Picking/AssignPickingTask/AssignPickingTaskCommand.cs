using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.AssignPickingTask;

public sealed record AssignPickingTaskCommand(Guid TaskHeaderId, Guid WarehouseOperatorId) : ICommand<Guid>;
