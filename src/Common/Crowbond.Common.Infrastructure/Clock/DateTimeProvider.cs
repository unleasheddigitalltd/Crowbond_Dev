using Crowbond.Common.Application.Clock;

namespace Crowbond.Common.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    private DateTime? _overrideUtcNow;

    public DateTime UtcNow => _overrideUtcNow ?? DateTime.UtcNow;

    public Task SetOverrideUtcNow(DateTime dateTime)
    {
        _overrideUtcNow = dateTime;
        return Task.CompletedTask;
    }
    public Task ClearOverrideUtcNow()
    {
        _overrideUtcNow = null;
        return Task.CompletedTask;
    }
}
