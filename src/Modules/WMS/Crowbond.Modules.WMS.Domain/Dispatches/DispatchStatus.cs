namespace Crowbond.Modules.WMS.Domain.Dispatches;

public enum DispatchStatus
{
    NotStarted = 0,
    Processing = 1,
    Completed = 2,
    Shipped = 3,
    Cancelled = 4,
}
