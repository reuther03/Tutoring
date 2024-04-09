using Tutoring.Domain.Competences;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Application.Abstractions.Database.Repositories;

public interface ICompetenceGroupRepository
{
    Task<bool> ExistsWithNameAsync(Name name, CancellationToken cancellationToken = default);
    Task<CompetenceGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(CompetenceGroup competenceGroup);
    void Remove(CompetenceGroup competenceGroup);

    #region Competences

    Task<Competence?> GetCompetenceByIdAsync(CompetenceId competenceId, CancellationToken cancellationToken = default);
    Task<bool> IsCompetenceInUseAsync(CompetenceId competenceId, CancellationToken cancellationToken = default);
    void RemoveCompetence(Competence competence);

    #endregion
}