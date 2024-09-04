using Crowbond.Common.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Crowbond.Common.Infrastructure.ChangeDetection;

public sealed class ChangeDetectionInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        // Check if there are any IChangeDetectable entities in the context
        IEnumerable<EntityEntry<IChangeDetectable>> entries =
            eventData
                .Context
                .ChangeTracker
                .Entries<IChangeDetectable>();

        if (entries.Any())
        {
            // Call DetectChanges for entities that implement IChangeDetectable
            eventData.Context.ChangeTracker.DetectChanges();
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
