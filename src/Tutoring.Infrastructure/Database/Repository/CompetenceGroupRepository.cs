using Microsoft.EntityFrameworkCore;
using Tutoring.Application.Abstractions.Database.Repositories;
using Tutoring.Domain.Competences;
using Tutoring.Domain.Users;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Infrastructure.Database.Repository;

public class CompetenceGroupRepository : ICompetenceGroupRepository
{
    private readonly TutoringDbContext _context;

    public CompetenceGroupRepository(TutoringDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsWithNameAsync(Name name, CancellationToken cancellationToken = default)
        => await _context.CompetencesGroups.AnyAsync(x => x.Name == name, cancellationToken);

    public async Task<CompetenceGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context.CompetencesGroups
            .Include(x => x.Competences)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public Task<CompetenceGroup?> GetByCompetenceIdAsync(CompetenceId competenceId, CancellationToken cancellationToken = default)
        => _context.CompetencesGroups
            .Include(x => x.Competences)
            .FirstOrDefaultAsync(x => x.Competences.Any(c => c.Id == competenceId), cancellationToken);

    public void Add(CompetenceGroup competenceGroup) => _context.CompetencesGroups.Add(competenceGroup);

    public void Remove(CompetenceGroup competenceGroup)
        => _context.CompetencesGroups.Remove(competenceGroup);

    #region Competences

    public Task<Competence?> GetCompetenceByIdAsync(CompetenceId competenceId, CancellationToken cancellationToken = default)
        => _context.CompetencesGroups
            .SelectMany(x => x.Competences)
            .FirstOrDefaultAsync(x => x.Id == competenceId, cancellationToken);

    public async Task<bool> IsCompetenceInUseAsync(CompetenceId competenceId, CancellationToken cancellationToken = default)
    {
        var isAssignedToTutor = await _context.Users.OfType<Tutor>()
            .AnyAsync(t => t.CompetenceIds.Any(c => c.Value == competenceId.Value), cancellationToken);

        if (isAssignedToTutor)
        {
            return true;
        }

        var isAssignedToSubject = await _context.Users.OfType<Student>()
            .Include(x => x.Subjects)
            .SelectMany(x => x.Subjects)
            .AnyAsync(s => s.CompetenceIds.Any(c => c.Value == competenceId.Value), cancellationToken);

        return isAssignedToSubject;
    }

    public void RemoveCompetence(Competence competence)
    {
        _context.Set<Competence>().Remove(competence);
    }

    #endregion
}