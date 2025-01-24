using Crowbond.Modules.WMS.Domain.Users;
using Crowbond.Modules.WMS.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.WMS.Infrastructure.Users;

internal sealed class UserRepository(WmsDbContext context) : IUserRepository
{
    public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public void Insert(User user)
    {
        context.Users.Add(user);
    }
}
