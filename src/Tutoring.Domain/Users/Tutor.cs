using Tutoring.Common.Exceptions.Domain;
using Tutoring.Domain.Availabilities;
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

    public static Tutor Create(Email email, Name firstname, Name lastname, Password password)
    {
        var tutor = new Tutor(UserId.New(), email, firstname, lastname, password);
        return tutor;
    }

    public void AddCompetence(CompetenceId competenceId)
    {
        if (_competenceIds.Contains(competenceId))
        {
            throw new DomainException("Competence already added.");
        }

        _competenceIds.Add(competenceId);
    }
}