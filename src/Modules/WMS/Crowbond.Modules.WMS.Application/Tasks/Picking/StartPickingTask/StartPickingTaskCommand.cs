using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.StartPickingTask;

public sealed record StartPickingTaskCommand(Guid UserId, Guid TaskHeaderId) : ICommand;
