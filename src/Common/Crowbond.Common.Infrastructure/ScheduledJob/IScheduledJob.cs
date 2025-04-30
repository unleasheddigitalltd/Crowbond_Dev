namespace Crowbond.Common.Infrastructure.ScheduledJob;

public interface IScheduledJob
{
    Task ExecuteAsync(CancellationToken cancellationToken = default);
}
