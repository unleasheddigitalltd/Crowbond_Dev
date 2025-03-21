﻿namespace Crowbond.Modules.Users.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Role?> GetRoleAsync(string name, CancellationToken cancellationToken = default);

    void Insert(User user);

    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

    Task<User?> GetByIdentityIdAsync(string identityId, CancellationToken cancellationToken = default);
}
