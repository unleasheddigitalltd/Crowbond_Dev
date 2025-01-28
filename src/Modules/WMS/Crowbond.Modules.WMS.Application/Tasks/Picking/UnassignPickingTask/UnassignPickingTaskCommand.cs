using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.UnassignPickingTask;

public sealed record UnassignPickingTaskCommand(Guid TaskHeaderId) : ICommand;
