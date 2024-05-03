using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Tutoring.Common.Primitives.Domain;

namespace Tutoring.Infrastructure.Database.Interceptors;

public class AggregateArchiveInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var changeTracker = eventData.Context?.ChangeTracker;

        if (changeTracker == null) return base.SavingChangesAsync(eventData, result, cancellationToken);
        foreach (var entry in changeTracker.Entries<IArchivable>())
        {
            if (entry.State is not EntityState.Deleted)
                continue;

            if (entry.Entity.IsArchived)
                continue;

            entry.State = EntityState.Modified;
            entry.Entity.SetArchiveData(true, DateTime.UtcNow);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}