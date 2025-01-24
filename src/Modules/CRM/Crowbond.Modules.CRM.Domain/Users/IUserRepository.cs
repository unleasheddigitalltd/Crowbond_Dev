namespace Crowbond.Modules.CRM.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(User user);
}
