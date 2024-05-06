using Tutoring.Common.Exceptions.Domain;
using Tutoring.Common.Primitives.Domain;
using Tutoring.Common.ValueObjects;
using Tutoring.Domain.Competences;

namespace Tutoring.Domain.Subjects;

public class Subject : Entity<Guid>
{
    private readonly List<CompetenceId> _competenceIds = [];

    public Description Description { get; private set; }
    public IReadOnlyList<CompetenceId> CompetenceIds => _competenceIds.AsReadOnly();

    private Subject()
    {
    }

    private Subject(Guid id, Description description) : base(id)
    {
        Description = description;
    }

    public static Subject Create(Description description)
    {
        var subject = new Subject(Guid.NewGuid(), description);
        return subject;
    }

    public void AddCompetence(Competence competence)
    {
        if (_competenceIds.Contains(competence.Id))
        {
            throw new DomainException("Competence already added.");
        }
        _competenceIds.Add(competence.Id);
    }

    public void RemoveCompetence(Competence competence)
    {
        if (!_competenceIds.Contains(competence.Id))
        {
            throw new DomainException("Competence not found.");
        }
        _competenceIds.Remove(competence.Id);
    }
}