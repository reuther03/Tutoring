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

    public CompetencesGroup(Guid id, Name name, Description description) : base(id)
    {
        Name = name;
        Description = description;
    }
}