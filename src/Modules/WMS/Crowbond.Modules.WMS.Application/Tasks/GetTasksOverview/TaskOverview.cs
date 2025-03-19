
namespace Crowbond.Modules.WMS.Application.Tasks.GetTasksOverview;

public sealed class TaskOverview(
    Guid taskId,
    string taskNo,
    string dispatchNo,
    Guid routeTripId,
    string routeName,
    DateTime routeTripDate,
    int taskType,
    int status,
    Guid assignedOperatorName,
    long totalLines,
    long completedLines,
    long rowNum)
{
    public Guid TaskId { get; } = taskId;
    public string TaskNo { get; } = taskNo;
    public string DispatchNo { get; } = dispatchNo;
    public Guid RouteTripId { get; } = routeTripId;
    public string RouteName { get; } = routeName;
    public DateTime RouteTripDate { get; } = routeTripDate;
    public int TaskType { get; } = taskType;
    public int Status { get; } = status;
    public Guid AssignedOperatorName { get; } = assignedOperatorName;
    public long TotalLines { get; } = totalLines;
    public long CompletedLines { get; } = completedLines;
    public long RowNum { get; } = rowNum;
}
