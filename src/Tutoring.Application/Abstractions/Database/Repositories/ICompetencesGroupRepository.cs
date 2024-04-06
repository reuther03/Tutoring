using Tutoring.Domain.Competences;

namespace Tutoring.Application.Abstractions.Database.Repositories;

public interface ICompetencesGroupRepository
{
    Task<bool> ExistsWithNameAsync(string name, CancellationToken cancellationToken = default);
    Task<CompetencesGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(CompetencesGroup competencesGroup, CancellationToken cancellationToken = default);
}