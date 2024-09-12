using Microsoft.Extensions.Options;
using Quartz;

namespace Crowbond.Modules.CRM.Infrastructure.CustomerProductPriceUpdating;

internal sealed class ConfigureProcessCustomerProductPriceUpdatingJob(IOptions<CustomerProductPriceUpdatingOptions> options)
    : IConfigureOptions<QuartzOptions>
{
    private readonly CustomerProductPriceUpdatingOptions _options = options.Value;

    public void Configure(QuartzOptions options)
    {
        string jobName = typeof(ProcessCustomerProductPriceUpdatingJob).FullName!;


        options
            .AddJob<ProcessCustomerProductPriceUpdatingJob>(configure => configure.WithIdentity(jobName))
            .AddTrigger(configure =>
                configure
                    .ForJob(jobName)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(_options.IntervalInSeconds)
                        .RepeatForever()
                    ));

    }
}
