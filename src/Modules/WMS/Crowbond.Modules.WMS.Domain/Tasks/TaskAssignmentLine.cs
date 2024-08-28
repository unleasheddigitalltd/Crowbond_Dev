using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Tasks;

public sealed class TaskAssignmentLine : Entity
{
    private TaskAssignmentLine()
    {
    }

    public Guid Id { get; private set; }

    public Guid TaskAssignmentId { get; private set; }

    public DateTime? StartDateTime { get; private set; }

    public DateTime? EndDateTime { get; private set; }

    public Guid FromLocationId { get; private set; }

    public Guid? ToLocationId { get; private set; }

    public Guid ProductId { get; private set; }

    public decimal RequestedQty { get; private set; }

    public decimal? CompletedQty { get; private set; }

    public decimal? MissedQty { get; private set; }

    public TaskAssignmentLineStatus Status { get; private set; }

    public static TaskAssignmentLine Create(
        Guid taskAssignmentId,
        Guid fromLocationId,
        Guid productId,
        decimal requestedQty)
    {
        var taskAssignmentLine = new TaskAssignmentLine
        {
            Id = Guid.NewGuid(),
            TaskAssignmentId = taskAssignmentId,
            FromLocationId = fromLocationId,
            ProductId = productId,
            RequestedQty = requestedQty,
            Status = TaskAssignmentLineStatus.Notstarted
        };

        return taskAssignmentLine;
    }

}
