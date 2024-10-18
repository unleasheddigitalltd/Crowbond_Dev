namespace Crowbond.Modules.WMS.Application.Tasks.Picking.GetMyPickingTaskAssignmentLineDatails;

public sealed record TaskAssignmentLineDetailsResponse(
    Guid Id,
    Guid ProductId,
    string ProductName,
    string ProductSku,
    decimal RequestedQty,
    decimal CompletedQty);
