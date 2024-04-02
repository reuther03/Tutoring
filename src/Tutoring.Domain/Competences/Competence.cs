using Tutoring.Common.Primitives.Domain;
using Tutoring.Common.ValueObjects;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Domain.Competences;

public class Competence : Entity<Guid>
{
    public Name DetailedName { get; private set; }
    public Description Description { get; private set; }

    public Competence(Guid id, Name detailedName, Description description) : base(id)
    {
        DetailedName = detailedName;
        Description = description;
    }
}