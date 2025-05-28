using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Dispatches;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.Picking.CompletePickingLine;

public sealed class CompletePickingLineCommandHandler(
    ITaskRepository taskRepository,
    IDispatchRepository dispatchRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CompletePickingLineCommand>
{
    public async Task<Result> Handle(CompletePickingLineCommand request, CancellationToken cancellationToken)
    {
        // Get the task header
        var taskHeader = await taskRepository.GetAsync(request.TaskHeaderId, cancellationToken);

        if (taskHeader is null)
        {
            return Result.Failure(TaskErrors.NotFound(request.TaskHeaderId));
        }

        // Check if the user is assigned to this task
        if (!taskHeader.IsAssignedTo(request.UserId))
        {
            return Result.Failure(TaskErrors.ActiveAssignmentForOperatorNotFound(request.UserId));
        }

        // Get the task line
        var taskLine = await taskRepository.GetTaskLineAsync(request.TaskLineId, cancellationToken);

        if (taskLine is null)
        {
            return Result.Failure(TaskErrors.TaskLineNotFound(request.TaskLineId));
        }

        // Complete the task line with the specified quantity
        var completeResult = taskHeader.CompleteTaskLine(request.TaskLineId, request.CompletedQty);
        
        if (completeResult.IsFailure)
        {
            return completeResult;
        }

        // Update all the related dispatch lines
        foreach (var mapping in taskLine.DispatchMappings)
        {
            // Calculate the proportion of the completed quantity for this dispatch line
            var proportionOfTotal = mapping.AllocatedQty / taskLine.TotalQty;
            var dispatchLineCompletedQty = request.CompletedQty * proportionOfTotal;
            
            // Get the dispatch header for this dispatch line
            var dispatchLine = await dispatchRepository.GetLineAsync(mapping.DispatchLineId, cancellationToken);
            
            if (dispatchLine is null)
            {
                continue;
            }
            
            var dispatch = await dispatchRepository.GetAsync(dispatchLine.DispatchHeaderId, cancellationToken);
            
            if (dispatch is null)
            {
                continue;
            }

            // Update the dispatch line
            var pickResult = dispatch.PickLine(mapping.DispatchLineId, dispatchLineCompletedQty);
            
            if (pickResult.IsFailure)
            {
                return pickResult;
            }
            
            // If all lines for this dispatch are picked, finalize them
            if (dispatch.Lines.All(l => l.IsPicked))
            {
                foreach (var line in dispatch.Lines)
                {
                    dispatch.FinalizeLinePicking(line.Id);
                }
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
