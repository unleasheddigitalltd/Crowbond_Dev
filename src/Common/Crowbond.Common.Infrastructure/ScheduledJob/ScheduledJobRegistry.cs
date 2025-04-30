using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Crowbond.Common.Infrastructure.ScheduledJob;

public static class ScheduledJobRegistry
{
    /// <summary>
    /// Registers a scheduled job and its trigger with Quartz using the provided DI service collection.
    /// </summary>
    /// <typeparam name="TJob">The job type (must implement IJob).</typeparam>
    /// <param name="services">The DI service collection.</param>
    /// <param name="jobName">A unique name for the job.</param>
    /// <param name="interval">The interval between job executions.</param>
    public static void RegisterScheduledJob<TJob>(
        IServiceCollection services,
        string jobName,
        TimeSpan interval
    )
        where TJob : class, IJob
    {
        services.AddQuartz(q =>
        {
            var jobKey = new JobKey(jobName);
            q.AddJob<TJob>(opts => opts.WithIdentity(jobKey));
            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithSimpleSchedule(x => x.WithInterval(interval).RepeatForever()));
        });

        services.AddTransient<TJob>();
    }
}
