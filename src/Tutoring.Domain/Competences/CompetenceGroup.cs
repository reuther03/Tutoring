using Tutoring.Common.Primitives.Domain;
using Tutoring.Common.ValueObjects;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Domain.Competences;

public class CompetenceGroup : AggregateRoot<Guid>
{
    private readonly List<Competence> _competences = [];

    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public IReadOnlyList<Competence> Competences => _competences.AsReadOnly();

    private CompetenceGroup()
    {
    }

    private CompetenceGroup(Guid id, DateTime? archivedAt, bool isArchived, Name name, Description description)
        : base(id, archivedAt, isArchived)
    {
        Name = name;
        Description = description;
    }

    public static CompetenceGroup Create(Name name, Description description)
    {
        var competencesGroup = new CompetenceGroup(Guid.NewGuid(), null, false, name, description);
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


    public void RemoveCompetence(Competence competence)
    {
        _competences.Remove(competence);
        // RaiseDomainEvent(new CompetenceRemovedDomainEvent(competence.Id));
    }

    // TODO: xd
    // public void RemoveCompetence(CompetenceId competenceId)
    // {
    //     var competence = _competences.FirstOrDefault(x => x.Id == competenceId);
    //     if (competence is not null)
    //         _competences.Remove(competence);
    // }
}