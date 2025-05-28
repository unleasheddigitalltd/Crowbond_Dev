using Crowbond.Common.Domain;

namespace Crowbond.Modules.WMS.Domain.Tasks;

public sealed class TaskLineDispatchMapping : Entity
{
    private TaskLineDispatchMapping()
    {
    }

    public Guid Id { get; private set; }

    public Guid TaskLineId { get; private set; }

    public Guid DispatchLineId { get; private set; }

    public decimal AllocatedQty { get; private set; }

    public decimal CompletedQty { get; private set; }

    public bool IsCompleted => CompletedQty >= AllocatedQty;

    public TaskLine TaskLine { get; }

    public static TaskLineDispatchMapping Create(
        Guid taskLineId,
        Guid dispatchLineId,
        decimal allocatedQty)
    {
        var mapping = new TaskLineDispatchMapping
        {
            Id = Guid.NewGuid(),
            TaskLineId = taskLineId,
            DispatchLineId = dispatchLineId,
            AllocatedQty = allocatedQty,
            CompletedQty = 0
        };

        return mapping;
    }

    public Result CompleteQuantity(decimal qty)
    {
        if (CompletedQty + qty > AllocatedQty)
        {
            return Result.Failure(TaskErrors.MappingCompleteQtyExceedsAllocatedQty(Id));
        }

        CompletedQty += qty;
        return Result.Success();
    }
}
    
