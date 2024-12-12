using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Tasks;

public sealed class TaskAssignmentLine : Entity
{
    private TaskAssignmentLine()
    {
    }

    public Guid Id { get; private set; }

    public Guid TaskAssignmentId { get; private set; }

    public Guid? ReceiptLineId { get; private set; }

    public Guid? DispatchLineId { get; private set; }

    public Guid FromLocationId { get; private set; }

    public Guid ToLocationId { get; private set; }

    public Guid ProductId { get; private set; }

    public decimal Qty { get; private set; }

    public TaskAssignment Assignment { get; }

    internal static TaskAssignmentLine Create(
        Guid? receiptLineId,
        Guid? dispatchLineId,
        Guid fromLocationId,
        Guid toLocationId,
        Guid productId,
        decimal qty)
    {
        var taskAssignmentLine = new TaskAssignmentLine
        {
            Id = Guid.NewGuid(),
            ReceiptLineId = receiptLineId,
            DispatchLineId = dispatchLineId,
            FromLocationId = fromLocationId,
            ToLocationId = toLocationId,
            ProductId = productId,
            Qty = qty
        };

        return taskAssignmentLine;
    }
}
