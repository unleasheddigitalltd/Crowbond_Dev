namespace Crowbond.Common.Domain;

public interface IJobExecutionTracking
{
    DateTime? ProcessedOnUtc { get; set; }
    string? ErrorMessage { get; set; }
}
