using Microsoft.Extensions.Options;
using Quartz;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerPriceUpdating;

internal sealed class ConfigureProcessCustomerPriceUpdatingJob(IOptions<CustomerPriceUpdatingOptions> options)
    : IConfigureOptions<QuartzOptions>
{
    private readonly CustomerPriceUpdatingOptions _options = options.Value;

    public void Configure(QuartzOptions options)
    {
        string jobName = typeof(ProcessCustomerPriceUpdatingJob).FullName!;


        options
            .AddJob<ProcessCustomerPriceUpdatingJob>(configure => configure.WithIdentity(jobName))
            .AddTrigger(configure =>
                configure
                    .ForJob(jobName)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(_options.IntervalInSeconds)
                        .RepeatForever()
                    ));

    }
}
