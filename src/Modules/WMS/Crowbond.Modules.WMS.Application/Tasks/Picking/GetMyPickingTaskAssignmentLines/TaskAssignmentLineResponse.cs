using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetMyPickingTaskAssignmentLines;

public sealed record TaskAssignmentLineResponse(
    Guid TaskAssignmentLineId,
    Guid ProductId,
    string ProductSku,
    string ProductName,
    string UnitOfMeasure,
    decimal RequestedQty,
    decimal CompletedQty,
    TaskAssignmentLineStatus Status);
