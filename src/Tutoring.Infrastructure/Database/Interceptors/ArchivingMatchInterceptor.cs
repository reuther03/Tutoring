using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Tutoring.Domain.Matchings;

namespace Tutoring.Infrastructure.Database.Interceptors;

public class ArchivingMatchInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var changeTracker = eventData.Context?.ChangeTracker;

        if (changeTracker == null) return base.SavingChangesAsync(eventData, result, cancellationToken);
        foreach (var entry in changeTracker.Entries<Matching>())
        {
            if (entry.State != EntityState.Modified) continue;

            if (entry.Entity.IsArchived) continue;
            entry.Entity.IsArchived = true;
            entry.Entity.ArchivedAt = DateTime.UtcNow;
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}