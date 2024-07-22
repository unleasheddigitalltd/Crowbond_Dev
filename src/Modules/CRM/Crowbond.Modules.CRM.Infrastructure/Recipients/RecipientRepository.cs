using Crowbond.Modules.CRM.Domain.Recipients;
using Crowbond.Modules.CRM.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Crowbond.Modules.CRM.Infrastructure.Recipients;

internal sealed class RecipientRepository(CrmDbContext context) : IRecipientRepository
{
    public async Task<Recipient?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Recipients.SingleOrDefaultAsync(recipient => recipient.Id == id, cancellationToken);
    }

    public void Insert(Recipient recipient)
    {
        context.Recipients.Add(recipient);
    }
}
