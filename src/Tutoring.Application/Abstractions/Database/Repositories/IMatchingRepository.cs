using Tutoring.Domain.Matchings;

namespace Tutoring.Application.Abstractions.Database.Repositories;

public interface IMatchingRepository
{
    Task<Matching?> GetMatchingByIdAsync(Guid matchingId, CancellationToken cancellationToken = default);
    Task AddAsync(Matching matching, CancellationToken cancellationToken = default);
    void ArchiveMatching(Matching matching);
}