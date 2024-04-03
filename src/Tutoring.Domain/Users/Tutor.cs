using Tutoring.Domain.Competences;
using Tutoring.Domain.Users.ValueObjects;

namespace Tutoring.Domain.Users;

public sealed class Tutor : User
{
    private readonly List<CompetenceId> _competenceIds = [];

    public IReadOnlyList<CompetenceId> CompetenceIds => _competenceIds.AsReadOnly();

    private Tutor()
    {
    }

    private Tutor(UserId id, Email email, Name firstname, Name lastname, Password password)
        : base(id, email, firstname, lastname, password, Role.Tutor)
    {
    }

    public static Tutor Create(UserId id, Email email, Name firstname, Name lastname, Password password)
    {
        var tutor = new Tutor(id, email, firstname, lastname, password);
        return tutor;
    }
}