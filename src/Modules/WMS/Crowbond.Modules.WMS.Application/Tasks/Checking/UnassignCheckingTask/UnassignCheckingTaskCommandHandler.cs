using Crowbond.Common.Application.Clock;
using Crowbond.Common.Application.Messaging;
using Crowbond.Common.Domain;
using Crowbond.Modules.WMS.Application.Abstractions.Data;
using Crowbond.Modules.WMS.Domain.Tasks;

namespace Crowbond.Modules.WMS.Application.Tasks.Checking.UnassignCheckingTask;

internal sealed class UnassignCheckingTaskCommandHandler(
    ITaskRepository taskRepository,
    IDateTimeProvider dateTimeProvider,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UnassignCheckingTaskCommand>
{
    public async Task<Result> Handle(UnassignCheckingTaskCommand request, CancellationToken cancellationToken)
    {
        TaskHeader? taskHeader = await taskRepository.GetAsync(request.TaskHeaderId, cancellationToken);

        if (taskHeader is null)
        {
            return Result.Failure(TaskErrors.NotFound(request.TaskHeaderId));
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
