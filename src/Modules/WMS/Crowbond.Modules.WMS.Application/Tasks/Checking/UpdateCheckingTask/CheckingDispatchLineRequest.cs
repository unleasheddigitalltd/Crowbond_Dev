namespace Crowbond.Modules.WMS.Application.Tasks.Checking.UpdateCheckingTask;

public sealed record CheckingDispatchLineRequest(Guid DispatchLineId, bool IsChecked);
