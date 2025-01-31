using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.CompletePickingDispatchLine;

public sealed record CompletePickingDispatchLineCommand(Guid UserId, Guid TaskHeaderId, Guid DispatchLineId) : ICommand;
