using Crowbond.Common.Application.Messaging;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.CompletePickingLine;

public record CompletePickingLineCommand(
    Guid UserId,
    Guid TaskHeaderId,
    Guid TaskLineId,
    decimal CompletedQty) : ICommand;
