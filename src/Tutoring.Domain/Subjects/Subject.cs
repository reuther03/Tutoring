using Tutoring.Common.Primitives.Domain;
using Tutoring.Common.ValueObjects;
using Tutoring.Domain.Competences;

namespace Tutoring.Domain.Subjects;

public class Subject : Entity<Guid>
{
    private readonly List<Competence> _competenceId = [];

    public Description Description { get; private set; }
    public IReadOnlyList<Competence> Competence => _competenceId.AsReadOnly();

    private Subject(Description description)
    {
        Description = description;
    }
}