using Quartz;

namespace Crowbond.Common.Infrastructure.ScheduledJob;

public abstract class ScheduledJobBase : IJob, IScheduledJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await ExecuteAsync(context.CancellationToken);
    }

    public abstract Task ExecuteAsync(CancellationToken cancellationToken = default);
}
