namespace Crowbond.Modules.WMS.Application.Tasks.Checking.GetCheckingTaskDispatchLines;

public sealed record TaskDispatchLineResponse(
    Guid DispatchLineId,
    string ProductName,
    decimal OrderedQty,
    decimal PickedQty,
    bool IsPicked,
    bool IsChecked);
