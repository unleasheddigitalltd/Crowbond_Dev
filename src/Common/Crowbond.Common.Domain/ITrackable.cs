namespace Crowbond.Common.Domain;
public interface ITrackable
{
    Guid ChangedBy { get; set; }
}
