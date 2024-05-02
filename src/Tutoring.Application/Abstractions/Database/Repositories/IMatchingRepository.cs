using Tutoring.Domain.Matchings;

namespace Tutoring.Application.Abstractions.Database.Repositories;

public interface IMatchingRepository
{
    Task AddAsync(Matching matching, CancellationToken cancellationToken = default);
}