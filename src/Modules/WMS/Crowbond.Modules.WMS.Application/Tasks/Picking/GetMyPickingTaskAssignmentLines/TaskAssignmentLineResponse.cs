using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetMyPickingTaskAssignmentLines;

public sealed record TaskAssignmentLineResponse(
    Guid Id,
    string TaskNo,
    Guid ProductId,
    string ProductSku,
    string ProductName,
    string UnitOfMeasure,
    decimal RequestedQty,
    decimal CompletedQty,
    TaskAssignmentLineStatus Status);
