namespace Crowbond.Modules.CRM.Infrastructure.CustomerProductPriceUpdating;

internal sealed class CustomerProductPriceUpdatingOptions
{
    public int IntervalInSeconds { get; init; }

    public int BatchSize { get; init; }
}
