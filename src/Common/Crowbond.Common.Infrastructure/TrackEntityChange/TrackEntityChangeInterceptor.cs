﻿using Crowbond.Common.Application.Authentication;
using Crowbond.Common.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Crowbond.Common.Infrastructure.TrackEntityChange;
public sealed class TrackEntityChangeInterceptor(ICurrentUserContext currentUserContext) : SaveChangesInterceptor
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

        IEnumerable<EntityEntry<ITrackable>> entries =
            eventData
                .Context
                .ChangeTracker
                .Entries<ITrackable>();

        foreach (EntityEntry<ITrackable> auditable in entries)
        {
            auditable.Entity.ChangedBy = currentUserContext.UserId;
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
