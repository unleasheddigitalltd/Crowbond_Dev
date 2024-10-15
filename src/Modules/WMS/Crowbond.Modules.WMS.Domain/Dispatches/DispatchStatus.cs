namespace Crowbond.Modules.WMS.Domain.Dispatches;

public enum DispatchStatus
{
    NotStarted = 0,
    Processing = 1,
    Completed = 3,
    Shipped = 4,
    Cancelled = 5,
}
