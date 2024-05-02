using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Domain.Matchings;

namespace Tutoring.Infrastructure.Database.Repository;

public class MatchingRepository : IMatchingRepository
{
    private readonly TutoringDbContext _context;

    public MatchingRepository(TutoringDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Matching matching, CancellationToken cancellationToken = default)
        => await _context.Matchings.AddAsync(matching, cancellationToken);
}