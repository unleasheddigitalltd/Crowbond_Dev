using Crowbond.Common.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Crowbond.Common.Application.Authentication;

namespace Crowbond.Common.Infrastructure.AuditEntity;

public sealed class AuditEntityInterceptor(ICurrentUserContext currentUserContext) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(
                eventData, result, cancellationToken);
        }

        IEnumerable<EntityEntry<IAuditable>> entries =
            eventData
                .Context
                .ChangeTracker
                .Entries<IAuditable>();

        foreach (EntityEntry<IAuditable> auditable in entries)
        {
            if (auditable.State == EntityState.Added)
            {
                auditable.Entity.CreatedBy = currentUserContext.UserId;
                auditable.Entity.CreatedOnUtc = DateTime.UtcNow;
            }
            else if (auditable.State == EntityState.Modified)
            {
                auditable.Entity.LastModifiedBy = currentUserContext.UserId;
                auditable.Entity.LastModifiedOnUtc = DateTime.UtcNow;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
