namespace Crowbond.Common.Domain;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
    Guid? DeletedBy { get; set; }
    DateTime? DeletedOnUtc { get; set; }
}
