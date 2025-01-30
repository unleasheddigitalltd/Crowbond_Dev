using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.GetMyCheckingTaskAssignments;

public sealed record TaskAssignmentResponse
{
    public Guid TaskId { get; }
    public string TaskNo { get; }
    public string DispatchNo { get; }
    public Guid RouteTripId { get; }
    public string RouteName { get; }
    public DateTime RouteTripDate { get; }
    public TaskAssignmentStatus Status { get; }
    public string TaskType { get; }
    public long TotalLines { get; }
    public long CheckedLines { get; }
}
