using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Domain.Competences;

namespace Tutoring.Infrastructure.Database.Repository;

public class CompetencesGroupRepository : ICompetencesGroupRepository
{
    private readonly TutoringDbContext _context;

    public CompetencesGroupRepository(TutoringDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExistsWithNameAsync(string name, CancellationToken cancellationToken = default)
        => _context.CompetencesGroups.AnyAsync(x => x.Name == name, cancellationToken);

    public async Task<CompetencesGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.CompetencesGroups.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task AddAsync(CompetencesGroup competencesGroup, CancellationToken cancellationToken = default)
        => await _context.CompetencesGroups.AddAsync(competencesGroup, cancellationToken);
}