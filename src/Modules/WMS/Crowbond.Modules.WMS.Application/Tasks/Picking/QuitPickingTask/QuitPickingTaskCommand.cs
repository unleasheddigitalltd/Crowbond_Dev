using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.QuitPickingTask;

public sealed record QuitPickingTaskCommand(Guid UserId, Guid TaskHeaderId) : ICommand;
