namespace Crowbond.Common.Domain;

public interface IAuditable
{
    Guid CreatedBy { get; set; }
    DateTime CreatedOnUtc { get; set; }
    Guid? LastModifiedBy { get; set; }
    DateTime? LastModifiedOnUtc { get; set; }
}
