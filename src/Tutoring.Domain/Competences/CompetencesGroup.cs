using Tutoring.Common.Primitives.Domain;
using Tutoring.Common.ValueObjects;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Domain.Competences;

public class CompetencesGroup : Entity<Guid>
{
    private readonly List<Competence> _competences = [];

    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public IReadOnlyList<Competence> Competences => _competences.AsReadOnly();

    private CompetencesGroup()
    {
    }

    public CompetencesGroup(Guid id, Name name, Description description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public static CompetencesGroup Create(Name name, Description description)
    {
        var competencesGroup = new CompetencesGroup(Guid.NewGuid(), name, description);
        return competencesGroup;
    }

    public void AddCompetence(Competence competence)
    {
        _competences.Add(competence);
    }

    public void AddCompetences(IEnumerable<Competence> competences)
    {
        _competences.AddRange(competences);
    }
}