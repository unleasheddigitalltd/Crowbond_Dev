namespace Crowbond.Modules.CRM.Infrastructure.CustomerPriceUpdating;

internal sealed class CustomerPriceUpdatingOptions
{
    public TimeOnly StartAt { get; init; }

    public int IntervalInSeconds { get; init; }

    public int BatchSize { get; init; }
}
