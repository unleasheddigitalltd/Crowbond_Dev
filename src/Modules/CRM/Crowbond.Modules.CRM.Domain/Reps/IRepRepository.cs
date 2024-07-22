namespace Crowbond.Modules.CRM.Domain.Reps;

public interface IRepRepository
{
    Task<Rep?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Rep rep);
}
