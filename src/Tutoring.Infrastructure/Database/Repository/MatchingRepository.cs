using Microsoft.EntityFrameworkCore;
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

    public Task<Matching?> GetMatchingByIdAsync(Guid matchingId, CancellationToken cancellationToken = default)
        => _context.Matchings
            // .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == matchingId, cancellationToken);

    public async Task AddAsync(Matching matching, CancellationToken cancellationToken = default)
        => await _context.Matchings.AddAsync(matching, cancellationToken);

    public void RemoveMatching(Matching matching)
    {
        _context.Matchings.Remove(matching);
    }

    //TODO: czym sie to rozni
    // public void RemoveMatching(Matching matching)
    //     => _context.Set<Matching>().Remove(matching);
    //public void RemoveMatching(Matching matching)
//     {
//         _context.Matchings.Remove(matching);
//     }
// }
}