namespace Crowbond.Modules.CRM.Domain.Recipients;

public interface IRecipientRepository
{
    Task<Recipient?> GetAsync(Guid id, CancellationToken cancellationToken);

    void Insert(Recipient recipient);
}
