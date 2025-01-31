using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.PausePickingTask;

public sealed record PausePickingTaskCommand(Guid UserId, Guid TaskHeaderId) : ICommand;
