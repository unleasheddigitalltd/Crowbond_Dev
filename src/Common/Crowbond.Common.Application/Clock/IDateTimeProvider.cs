namespace Crowbond.Common.Application.Clock;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
    Task SetOverrideUtcNow(DateTime dateTime);
    Task ClearOverrideUtcNow();
}
