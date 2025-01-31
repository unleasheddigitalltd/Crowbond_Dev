using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.QuitCheckingTask;

internal sealed class QuitCheckingTaskCommandHandler(
    ITaskRepository taskRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<QuitCheckingTaskCommand>
{
    public async Task<Result> Handle(QuitCheckingTaskCommand request, CancellationToken cancellationToken)
    {
        TaskHeader? taskHeader = await taskRepository.GetAsync(request.TaskHeaderId, cancellationToken);

        if (taskHeader is null)
        {
            return Result.Failure(TaskErrors.NotFound(request.TaskHeaderId));
        }

        if (!taskHeader.IsAssignedTo(request.UserId))
        {
            return Result.Failure(TaskErrors.ActiveAssignmentForOperatorNotFound(request.UserId));
        }

        Result result = taskHeader.Quit(dateTimeProvider.UtcNow);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result;
    }
}
