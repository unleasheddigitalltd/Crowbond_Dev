using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.PutAway.GetPutAwayTaskAssignmentLines;

public sealed record TaskAssignmentLineResponse(
    Guid TaskAssignmentLineId,
    Guid ProductId,
    string ProductSku,
    string ProductName,
    string UnitOfMeasure,
    decimal RequestedQty,
    decimal CompletedQty,
    decimal MissedQty,
    TaskAssignmentLineStatus Status);
