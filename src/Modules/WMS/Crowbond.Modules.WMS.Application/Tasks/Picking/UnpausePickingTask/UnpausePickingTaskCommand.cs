using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.UnpausePickingTask;

public sealed record UnpausePickingTaskCommand(Guid UserId, Guid TaskHeaderId) : ICommand;
