using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Tasks;

public sealed class TaskAssignmentLine : Entity
{
    private TaskAssignmentLine()
    {
    }

    public Guid Id { get; private set; }

    public Guid TaskAssignmentId { get; private set; }

    public Guid ReceiptLineId { get; private set; }

    public DateTime? StartDateTime { get; private set; }

    public DateTime? EndDateTime { get; private set; }

    public Guid? FromLocationId { get; private set; }

    public Guid? ToLocationId { get; private set; }

    public Guid ProductId { get; private set; }

    public decimal RequestedQty { get; private set; }

    public decimal CompletedQty { get; private set; }

    public decimal MissedQty { get; private set; }

    public TaskAssignmentLineStatus Status { get; private set; }

    internal static TaskAssignmentLine Create(
        Guid productId,
        decimal requestedQty,
        Guid receiptLineId)
    {
        var taskAssignmentLine = new TaskAssignmentLine
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            RequestedQty = requestedQty,
            ReceiptLineId = receiptLineId,
            CompletedQty = 0,
            MissedQty = 0,
            Status = TaskAssignmentLineStatus.Notstarted
        };

        return taskAssignmentLine;
    }

    internal Result Start(DateTime modificationDate)
    {
        if (Status is not TaskAssignmentLineStatus.Notstarted)
        {
            return Result.Failure(TaskErrors.AlreadyStarted);
        }

        StartDateTime = modificationDate;
        Status = TaskAssignmentLineStatus.InProgress;

        return Result.Success();
    }

    internal Result Complete(DateTime modificationDate)
    {
        if (Status is not TaskAssignmentLineStatus.InProgress)
        {
            return Result.Failure(TaskErrors.LineNotInProgress);
        }

        MissedQty = RequestedQty - CompletedQty;
        Status = TaskAssignmentLineStatus.Completed;
        EndDateTime = modificationDate;

        return Result.Success();
    }

    internal Result Close(DateTime modificationDate) 
    {
        if (Status is TaskAssignmentLineStatus.Completed )
        {
            return Result.Failure(TaskErrors.LineIsCompleted);
        }

        Status = TaskAssignmentLineStatus.Incomplete;
        EndDateTime = modificationDate;

        return Result.Success();
    }

    internal Result IncrementCompletedQty(DateTime modificationDate, decimal Qty)
    {
        // check the qty is valid.
        if (Qty <= 0)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.QuantityNotGreaterThanZero);
        }

        // check the line is in progress
        if (Status != TaskAssignmentLineStatus.InProgress)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.LineForProductIsNotInProgress(ProductId));
        }

        // increment the complete quantity.
        CompletedQty += Qty;

        // check the compelet quantity is not greater then requested.
        if (RequestedQty < CompletedQty)
        {
            return Result.Failure<TaskAssignmentLine>(TaskErrors.ProductCompleteQtyExceedsRequestQty(ProductId));
        }

        // change the status to completed if the compelet quantity is equal to the requested.
        if (RequestedQty == CompletedQty)
        {
            Status = TaskAssignmentLineStatus.Completed;
            EndDateTime = modificationDate;
        }

        return Result.Success();
    }

}
