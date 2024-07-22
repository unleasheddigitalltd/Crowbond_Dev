using Crowbond.Modules.CRM.Domain.Reps;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.Reps;
internal sealed class RepRepository(CrmDbContext context) : IRepRepository
{
    public async Task<Rep?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Reps.SingleOrDefaultAsync(reps => reps.Id == id, cancellationToken);
    }

    public void Insert(Rep rep)
    {
        context.Reps.Add(rep);
    }
}
